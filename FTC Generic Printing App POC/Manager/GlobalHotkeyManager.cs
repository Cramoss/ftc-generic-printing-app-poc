using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Manager
{
    public class GlobalHotkeyManager : IDisposable
    {
        #region Win32 API
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Key modifiers
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_SHIFT = 0x0004;
        public const uint MOD_WIN = 0x0008;
        public const uint MOD_NOREPEAT = 0x4000;

        private const int WM_HOTKEY = 0x0312;
        #endregion

        private class HotkeyWindow : NativeWindow, IDisposable
        {
            private Dictionary<int, Action> hotkeyCallbacks = new Dictionary<int, Action>();

            public HotkeyWindow()
            {
                CreateHandle(new CreateParams());
            }

            public void RegisterCallback(int hotkeyId, Action callback)
            {
                hotkeyCallbacks[hotkeyId] = callback;
            }

            public void UnregisterCallback(int hotkeyId)
            {
                if (hotkeyCallbacks.ContainsKey(hotkeyId))
                {
                    hotkeyCallbacks.Remove(hotkeyId);
                }
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    int hotkeyId = m.WParam.ToInt32();
                    if (hotkeyCallbacks.ContainsKey(hotkeyId))
                    {
                        hotkeyCallbacks[hotkeyId]?.Invoke();
                    }
                }
            }

            public void Dispose()
            {
                this.DestroyHandle();
            }
        }

        private HotkeyWindow hotkeyWindow = new HotkeyWindow();
        private Dictionary<int, bool> registeredHotkeys = new Dictionary<int, bool>();
        private int currentId = 1;

        public GlobalHotkeyManager()
        {
        }

        public int RegisterHotkey(uint modifiers, Keys key, Action callback)
        {
            int id = currentId++;
            try
            {
                if (RegisterHotKey(hotkeyWindow.Handle, id, modifiers, (uint)key))
                {
                    hotkeyWindow.RegisterCallback(id, callback);
                    registeredHotkeys[id] = true;
                    return id;
                }
                else
                {
                    // Format the modifier keys
                    string modifierText = GetModifierText(modifiers);
                    int errorCode = Marshal.GetLastWin32Error();

                    // Log just once the error
                    Console.WriteLine($"Failed to register hotkey {modifierText}+{key}. Error code: {errorCode}");

                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception registering hotkey: {ex.Message}");
                return -1;
            }
        }

        // Helper method to convert modifier flags to readable text
        private string GetModifierText(uint modifiers)
        {
            List<string> modTexts = new List<string>();

            if ((modifiers & MOD_CONTROL) != 0) modTexts.Add("Ctrl");
            if ((modifiers & MOD_ALT) != 0) modTexts.Add("Alt");
            if ((modifiers & MOD_SHIFT) != 0) modTexts.Add("Shift");
            if ((modifiers & MOD_WIN) != 0) modTexts.Add("Win");

            return string.Join("+", modTexts);
        }

        public void UnregisterHotkey(int id)
        {
            if (id <= 0) return;

            if (registeredHotkeys.ContainsKey(id))
            {
                UnregisterHotKey(hotkeyWindow.Handle, id);
                hotkeyWindow.UnregisterCallback(id);
                registeredHotkeys.Remove(id);
            }
        }

        public void Dispose()
        {
            foreach (var id in registeredHotkeys.Keys)
            {
                UnregisterHotKey(hotkeyWindow.Handle, id);
            }
            registeredHotkeys.Clear();
            hotkeyWindow.Dispose();
        }
    }
}
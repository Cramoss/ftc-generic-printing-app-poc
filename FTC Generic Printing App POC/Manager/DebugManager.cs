using FTC_Generic_Printing_App_POC.Forms;
using System;
using System.Windows.Forms;
using FTC_Generic_Printing_App_POC.Utils;

namespace FTC_Generic_Printing_App_POC.Manager
{
    public class DebugManager : IDisposable
    {
        private static DebugManager instance;
        private static object syncRoot = new object();

        private DebugConsoleForm consoleForm;
        private GlobalHotkeyManager hotkeyManager;
        private int consoleHotkeyId;
        private bool isDisposed = false;

        public static DebugManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DebugManager();
                        }
                    }
                }
                return instance;
            }
        }

        private DebugManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                consoleForm = new DebugConsoleForm();
                hotkeyManager = new GlobalHotkeyManager();

                // Register Ctrl+Alt+D hotkey
                consoleHotkeyId = hotkeyManager.RegisterHotkey(
                    GlobalHotkeyManager.MOD_CONTROL | GlobalHotkeyManager.MOD_ALT,
                    Keys.D,
                    ToggleConsole);

                if (consoleHotkeyId != -1)
                {
                    AppLogger.LogInfo("Debug Console initialized. Press Ctrl+Alt+D to toggle console.");
                }
                else
                {
                    AppLogger.LogWarning("Failed to register Ctrl+Alt+D hotkey for Debug Console. The console will still be available but cannot be toggled with a hotkey.");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Failed to initialize debug console", ex);
            }
        }

        public void ToggleConsole()
        {
            if (consoleForm != null)
            {
                if (consoleForm.Visible)
                {
                    consoleForm.Hide();
                }
                else
                {
                    consoleForm.Show();
                    consoleForm.BringToFront();
                }
            }
        }

        public void AppendLog(string message)
        {
            if (!isDisposed && consoleForm != null)
            {
                consoleForm.AppendLog(message);
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;

                // Unregister hotkeys
                if (hotkeyManager != null)
                {
                    hotkeyManager.UnregisterHotkey(consoleHotkeyId);
                    hotkeyManager.Dispose();
                }

                // Dispose console form
                if (consoleForm != null)
                {
                    consoleForm.Hide();
                    consoleForm.Dispose();
                }

                instance = null;
            }
        }

        public static void ManualToggle()
        {
            Instance.ToggleConsole();
        }
    }
}
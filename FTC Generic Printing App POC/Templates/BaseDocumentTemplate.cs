using System;
using System.Collections.Generic;
using System.Text;
using FTC_Generic_Printing_App_POC.Interfaces;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // Base class for all document templates with common functionality and ESC/POS commands
    // This will be adjusted in the future as needed for different styles of documents
    public abstract class BaseDocumentTemplate : IDocumentTemplate
    {
        #region ESC/POS Commands
        protected static readonly byte[] ESC_INIT = { 0x1B, 0x40 };               // Initialize printer
        protected static readonly byte[] ESC_ALIGN_CENTER = { 0x1B, 0x61, 0x01 }; // Center alignment
        protected static readonly byte[] ESC_ALIGN_LEFT = { 0x1B, 0x61, 0x00 };   // Left alignment
        protected static readonly byte[] ESC_BOLD_ON = { 0x1B, 0x45, 0x01 };      // Bold on
        protected static readonly byte[] ESC_BOLD_OFF = { 0x1B, 0x45, 0x00 };     // Bold off
        protected static readonly byte[] ESC_DOUBLE_HEIGHT = { 0x1B, 0x21, 0x10 }; // Double height
        protected static readonly byte[] ESC_NORMAL = { 0x1B, 0x21, 0x00 };       // Normal text
        protected static readonly byte[] ESC_CUT = { 0x1D, 0x56, 0x41 };          // Cut paper
        protected static readonly byte[] LF = { 0x0A };                           // Line feed
        protected static readonly byte[] CR = { 0x0D };                           // Carriage return
        protected static readonly byte[] ESC_FLUSH = { 0x1B, 0x64, 0x00 };        // Flush buffer
        protected static readonly byte[] LF_MULTIPLE = { 0x1B, 0x64, 0x05 };      // Feed 5 lines
        protected static readonly byte[] ESC_CUT_FULL = { 0x1D, 0x56, 0x00 };     // Full cut
        protected static readonly byte[] ESC_CUT_PARTIAL = { 0x1D, 0x56, 0x01 };  // Partial cut
        #endregion

        public abstract string TemplateId { get; }

        public abstract List<byte[]> GenerateDocumentCommands(JObject document);

        #region Helper Methods
        protected byte[] TextLine(string text)
        {
            return Encoding.GetEncoding(850).GetBytes(text);
        }

        protected void AddText(List<byte[]> commands, string text)
        {
            commands.Add(TextLine(text));
            commands.Add(LF);
        }

        protected void AddCenteredText(List<byte[]> commands, string text)
        {
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(TextLine(text));
            commands.Add(LF);
            commands.Add(ESC_ALIGN_LEFT);
        }

        protected void AddHeaderText(List<byte[]> commands, string text)
        {
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_DOUBLE_HEIGHT);
            commands.Add(TextLine(text));
            commands.Add(LF);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_ALIGN_LEFT);
        }

        protected void AddDivider(List<byte[]> commands)
        {
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(TextLine("====================================="));
            commands.Add(LF);
            commands.Add(ESC_ALIGN_LEFT);
        }

        protected string SafeGetValue(JToken data, string property, string defaultValue = "")
        {
            try
            {
                if (data == null)
                    return defaultValue;

                var token = data[property];
                return token?.ToString() ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        protected void AddCutCommand(List<byte[]> commands)
        {
            // Adds several line feeds to ensure paper is positioned correctly
            commands.Add(LF);
            commands.Add(LF);
            commands.Add(LF);

            // Some printers need flush before cut
            commands.Add(ESC_FLUSH);

            // Try partial cut which is more compatible with most printers
            commands.Add(ESC_CUT_PARTIAL);

            // Some printers need flush after cut as well
            commands.Add(ESC_FLUSH);
        }
        
        // Helper method to wrap text for thermal printer width
        protected List<string> WrapText(string text, int maxWidth)
        {
            var lines = new List<string>();

            if (string.IsNullOrEmpty(text))
                return lines;

            // First split by existing line breaks
            var paragraphs = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

            foreach (var paragraph in paragraphs)
            {
                if (paragraph.Length <= maxWidth)
                {
                    lines.Add(paragraph);
                    continue;
                }

                var words = paragraph.Split(' ');
                var currentLine = "";

                foreach (var word in words)
                {
                    // If a single word is longer than maxWidth, we need to break it
                    if (word.Length > maxWidth)
                    {
                        // Add current line if it has content
                        if (currentLine.Length > 0)
                        {
                            lines.Add(currentLine);
                            currentLine = "";
                        }

                        // Break the long word
                        var tempWord = word;
                        while (tempWord.Length > maxWidth)
                        {
                            lines.Add(tempWord.Substring(0, maxWidth));
                            tempWord = tempWord.Substring(maxWidth);
                        }
                        currentLine = tempWord;
                    }
                    else if (currentLine.Length + word.Length + 1 <= maxWidth)
                    {
                        if (currentLine.Length > 0)
                            currentLine += " ";
                        currentLine += word;
                    }
                    else
                    {
                        if (currentLine.Length > 0)
                            lines.Add(currentLine);
                        currentLine = word;
                    }
                }

                if (currentLine.Length > 0)
                    lines.Add(currentLine);
            }

            return lines;
        }
        #endregion
    }
}

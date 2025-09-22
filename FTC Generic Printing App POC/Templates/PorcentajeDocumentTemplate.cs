using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTC_Generic_Printing_App_POC.Templates
{
    public class PorcentajeDocumentTemplate : BaseDocumentTemplate
    {
        public override string TemplateId => "porcentaje";

        // Additional ESC/POS commands not in base class
        private readonly byte[] ESC_TRIPLE_HEIGHT = new byte[] { 0x1B, 0x21, 0x30 }; // Triple height text
        private readonly byte[] ESC_QUAD_HEIGHT = new byte[] { 0x1B, 0x21, 0x60 }; // Quadruple height text
        private readonly byte[] ESC_MAX_HEIGHT = new byte[] { 0x1B, 0x21, 0x70 }; // Max height text
        private readonly byte[] ESC_UNDERLINE_ON = new byte[] { 0x1B, 0x2D, 0x01 };
        private readonly byte[] ESC_UNDERLINE_OFF = new byte[] { 0x1B, 0x2D, 0x00 };
        private readonly byte[] ESC_FONT_B = new byte[] { 0x1B, 0x4D, 0x01 }; // Smaller font
        private readonly byte[] ESC_FONT_A = new byte[] { 0x1B, 0x4D, 0x00 }; // Default font

        // Barcode specific commands
        private readonly byte[] GS_H = new byte[] { 0x1D, 0x48 }; // Set HRI character print position
        private readonly byte[] GS_h = new byte[] { 0x1D, 0x68 }; // Set barcode height
        private readonly byte[] GS_w = new byte[] { 0x1D, 0x77 }; // Set barcode width
        private readonly byte[] GS_k = new byte[] { 0x1D, 0x6B }; // Print barcode

        public override List<byte[]> GenerateDocumentCommands(JObject document)
        {
            var commands = new List<byte[]>();
            var config = ConfigurationManager.LoadTotemConfiguration();

            // Initialize printer
            commands.Add(ESC_INIT);
            commands.Add(ESC_ALIGN_CENTER);

            try
            {
                var dataToken = document["data"];

                if (dataToken != null)
                {
                    JToken turnoToken = dataToken["turno"];
                    if (turnoToken != null)
                    {
                        PrintTurnoSection(commands, turnoToken);
                    }

                    JToken cuponToken = dataToken["cupon"];
                    if (cuponToken != null)
                    {
                        PrintCuponSection(commands, cuponToken);
                    }
                }

                // Add extra spacing at the end
                commands.Add(LF);
                commands.Add(LF);
            }
            catch (Exception ex)
            {
                AddCenteredText(commands, "Error printing document");
                commands.Add(LF);
            }

            // Cut the paper
            AddCutCommand(commands);

            return commands;
        }

        private void PrintTurnoSection(List<byte[]> commands, JToken turno)
        {
            // Title: "Tu turno es"
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_QUAD_HEIGHT);
            commands.Add(TextLine("Tu turno es"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Turno number
            string turnoNumber = SafeGetValue(turno, "number", "---");
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_MAX_HEIGHT);
            commands.Add(TextLine(turnoNumber));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Footer text
            string footer = SafeGetValue(turno, "footer");
            if (!string.IsNullOrEmpty(footer))
            {
                commands.Add(ESC_NORMAL);
                var wrappedLines = WrapText(footer, 48);
                foreach (var line in wrappedLines)
                {
                    commands.Add(TextLine(line));
                    commands.Add(LF);
                }
            }

            // Add divider line
            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);
        }

        private void PrintCuponSection(List<byte[]> commands, JToken cupon)
        {
            commands.Add(ESC_ALIGN_CENTER);

            // Title
            string title = SafeGetValue(cupon, "title");
            if (!string.IsNullOrEmpty(title))
            {
                commands.Add(ESC_BOLD_ON);
                commands.Add(ESC_DOUBLE_HEIGHT);
                commands.Add(TextLine(title));
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_BOLD_OFF);
                commands.Add(LF);
                commands.Add(LF);
            }

            // Promocion percentage
            string promocion = SafeGetValue(cupon, "promocion");
            if (!string.IsNullOrEmpty(promocion))
            {
                commands.Add(ESC_BOLD_ON);
                commands.Add(ESC_MAX_HEIGHT);
                commands.Add(TextLine(promocion));
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_BOLD_OFF);
                commands.Add(LF);
                commands.Add(LF);
            }

            // Promocion footer
            string promocionFooter = SafeGetValue(cupon, "promocion_footer");
            if (!string.IsNullOrEmpty(promocionFooter))
            {
                commands.Add(ESC_BOLD_ON);
                commands.Add(ESC_DOUBLE_HEIGHT);
                commands.Add(TextLine(promocionFooter));
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_BOLD_OFF);
                commands.Add(LF);
                commands.Add(LF);
            }

            // Description
            string description = SafeGetValue(cupon, "description");
            if (!string.IsNullOrEmpty(description))
            {
                commands.Add(ESC_NORMAL);
                var wrappedLines = WrapText(description, 48);
                foreach (var line in wrappedLines)
                {
                    commands.Add(TextLine(line));
                    commands.Add(LF);
                }
            }

            // Divider
            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);

            // Exclusiones
            string exclusiones = SafeGetValue(cupon, "exclusiones");
            if (!string.IsNullOrEmpty(exclusiones))
            {
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_ALIGN_CENTER);
                var wrappedLines = WrapText(exclusiones, 48);
                foreach (var line in wrappedLines)
                {
                    commands.Add(TextLine(line));
                    commands.Add(LF);
                }

                commands.Add(LF);
                AddDivider(commands);
                commands.Add(LF);
            }

            // Barcode
            string barcode = SafeGetValue(cupon, "barcode");
            if (!string.IsNullOrEmpty(barcode))
            {
                PrintBarcode(commands, barcode);
                commands.Add(LF);
                commands.Add(LF);
            }

            // Footer
            string footerText = SafeGetValue(cupon, "footer");
            if (!string.IsNullOrEmpty(footerText))
            {
                // Remove HTML tags (simple approach for <br>)
                footerText = footerText.Replace("<br>", " ").Replace("<BR>", " ");

                commands.Add(ESC_FONT_A);
                var wrappedLines = WrapText(footerText, 40);
                foreach (var line in wrappedLines)
                {
                    commands.Add(TextLine(line));
                    commands.Add(LF);
                }
            }

            // Reset alignment
            commands.Add(ESC_ALIGN_LEFT);
        }

        private void PrintBarcode(List<byte[]> commands, string barcodeData)
        {
            try
            {
                // Configure barcode
                commands.Add(ESC_ALIGN_CENTER);

                // Set HRI character print position (below barcode)
                commands.Add(new byte[] { 0x1D, 0x48, 0x02 });

                // Set barcode height
                commands.Add(new byte[] { 0x1D, 0x68, 200 });

                // Set barcode width
                commands.Add(new byte[] { 0x1D, 0x77, 0x04 });

                // Print CODE128 barcode
                commands.Add(new byte[] { 0x1D, 0x6B, 0x08 });  // 0x08 = CODE128 in many printers
                commands.Add(Encoding.ASCII.GetBytes(barcodeData));
                commands.Add(new byte[] { 0x00 });  // NUL terminator
            }
            catch (Exception)
            {
                // Print the number as text in case barcode fails
                commands.Add(ESC_BOLD_ON);
                commands.Add(TextLine(barcodeData));
                commands.Add(ESC_BOLD_OFF);
            }
        }

        // Helper method to wrap text for thermal printer width
        private List<string> WrapText(string text, int maxWidth)
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
    }
}
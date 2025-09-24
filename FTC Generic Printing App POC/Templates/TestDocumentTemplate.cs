using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // Test document template for verifying printer functionality
    // Based on the Porcentaje template but with fake hardcoded data and additional ESC/POS commands
    public class TestDocumentTemplate : BaseDocumentTemplate
    {
        public override string TemplateId => "test";

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
                // Header
                commands.Add(ESC_BOLD_ON);
                commands.Add(ESC_MAX_HEIGHT);
                commands.Add(TextLine("DOCUMENTO DE PRUEBA"));
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_BOLD_OFF);
                commands.Add(LF);
                commands.Add(LF);

                AddDivider(commands);
                commands.Add(LF);
                commands.Add(LF);

                PrintTurnoSection(commands);

                // Display totem configuration if configured
                if (!string.IsNullOrEmpty(config.IdTotem))
                {
                    commands.Add(ESC_ALIGN_LEFT);
                    commands.Add(ESC_BOLD_ON);
                    commands.Add(TextLine("CONFIGURACIÓN DEL TOTEM ACTUAL"));
                    commands.Add(ESC_BOLD_OFF);

                    AddText(commands, $"ID Totem: {config.IdTotem}");
                    AddText(commands, $"Tienda: {config.Store} ({config.StoreId})");
                    AddText(commands, $"País: {config.Country}");
                    AddText(commands, $"Negocio: {config.Business}");
                    commands.Add(LF);
                    commands.Add(LF);
                    commands.Add(ESC_ALIGN_CENTER);
                }

                PrintCuponSection(commands);

                commands.Add(LF);
                commands.Add(LF);

                // Store information
                commands.Add(ESC_ALIGN_CENTER);
                commands.Add(ESC_NORMAL);
                commands.Add(ESC_FONT_B);
                commands.Add(TextLine($"Impreso: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}"));
                commands.Add(TextLine("FTC Generic Printing App POC"));
                commands.Add(LF);
                commands.Add(ESC_FONT_A);
            }
            catch (Exception ex)
            {
                AddCenteredText(commands, "Error printing test document");
                AddCenteredText(commands, ex.Message);
                commands.Add(LF);
            }

            // Cut the paper
            AddCutCommand(commands);

            return commands;
        }

        private void PrintTurnoSection(List<byte[]> commands)
        {
            // Title
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_QUAD_HEIGHT);
            commands.Add(TextLine("Tu turno es"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Turno number
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_MAX_HEIGHT);
            commands.Add(TextLine("A15"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Footer
            string footer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras dapibus lacus vel magna feugiat, in varius ipsum mollis.";
            commands.Add(ESC_NORMAL);
            foreach (var line in WrapText(footer, 48))
            {
                commands.Add(TextLine(line));
                commands.Add(LF);
            }

            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);
        }

        private void PrintCuponSection(List<byte[]> commands)
        {
            commands.Add(ESC_ALIGN_CENTER);

            // Title
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_DOUBLE_HEIGHT);
            commands.Add(TextLine("SOLO POR HOY"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Promocion percentage
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_MAX_HEIGHT);
            commands.Add(TextLine("25%"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Promocion footer
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_DOUBLE_HEIGHT);
            commands.Add(TextLine("DE DESCUENTO"));
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Description
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt, urna eget vulputate posuere, lectus risus euismod turpis, non feugiat felis magna vel orci. Donec malesuada ex in felis tempor, eu sagittis justo imperdiet.";
            commands.Add(ESC_NORMAL);
            foreach (var line in WrapText(description, 48))
            {
                commands.Add(TextLine(line));
                commands.Add(LF);
            }

            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);

            // Exclusiones
            string exclusiones = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.";
            commands.Add(ESC_NORMAL);
            commands.Add(ESC_ALIGN_CENTER);
            foreach (var line in WrapText(exclusiones, 48))
            {
                commands.Add(TextLine(line));
                commands.Add(LF);
            }

            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);

            string barcodeData = "123456789012";
            PrintBarcode(commands, barcodeData);
            commands.Add(LF);
            commands.Add(LF);

            // Footer
            string footerText = "Nullam euismod urna vel ex dignissim, non bibendum lorem condimentum. Integer at vestibulum eros. Vivamus vulputate augue vel lorem finibus eleifend.";
            commands.Add(ESC_FONT_A);
            foreach (var line in WrapText(footerText, 40))
            {
                commands.Add(TextLine(line));
                commands.Add(LF);
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

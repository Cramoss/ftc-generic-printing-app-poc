using System;
using System.Collections.Generic;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // TODO: Adjust this template to replicate current Porcentaje document format.
    // TODO: Add barcode support.
    // Use the SafeGetValue method to extract data from the JSON payload. Current messages are placeholders.
    public class PorcentajeDocumentTemplate : BaseDocumentTemplate
    {
        public override string TemplateId => "porcentaje";

        public override List<byte[]> GenerateDocumentCommands(dynamic document)
        {
            var commands = new List<byte[]>();
            var config = ConfigurationManager.LoadTotemConfiguration();

            // Initialize printer
            commands.Add(ESC_INIT);

            // First custom message
            AddCenteredText(commands, "BIENVENIDO");
            commands.Add(LF);

            // Document number in bigger text
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_DOUBLE_HEIGHT);
            AddText(commands, "A01");
            commands.Add(ESC_BOLD_OFF);
            commands.Add(ESC_NORMAL);
            commands.Add(LF);
            commands.Add(LF);

            // Second custom message
            AddCenteredText(commands, "Proin elementum ligula in molestie interdum. " +
                "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.");
            commands.Add(LF);

            // Third custom message
            AddCenteredText(commands, "Proin elementum ligula in molestie interdum.");
            commands.Add(LF);

            // Percentage in normal text
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            AddText(commands, "60%");
            commands.Add(ESC_BOLD_OFF);
            commands.Add(LF);
            commands.Add(LF);

            // Footer
            AddDivider(commands);
            AddCenteredText(commands, "Senectus et netus et malesuada fames ac turpis egestas");
            commands.Add(LF);

            // Cut the paper
            AddCutCommand(commands);

            return commands;
        }
    }
}
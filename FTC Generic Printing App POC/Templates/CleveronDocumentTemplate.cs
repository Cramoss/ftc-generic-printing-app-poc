using FTC_Generic_Printing_App_POC.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FTC_Generic_Printing_App_POC.Templates
{
    public class CleveronDocumentTemplate : BaseDocumentTemplate
    {
        public override string TemplateId => "cleveron";

        private readonly byte[] ESC_MAX_HEIGHT = new byte[] { 0x1B, 0x21, 0x70 };

        public override List<byte[]> GenerateDocumentCommands(JObject document)
        {
            var commands = new List<byte[]>();

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
                        PrintTurnoFooter(commands, turnoToken);
                    }

                    // TODO: Remove POC on final version
                    PrintLogoImage(commands, "FTC_Generic_Printing_App_POC.Resources.cleveron_locker_optimized.png");
                }

                commands.Add(LF);
                commands.Add(LF);
            }
            catch (Exception ex)
            {
                AddCenteredText(commands, "Error printing document");
                commands.Add(LF);
            }

            AddCutCommand(commands);

            return commands;
        }

        private void PrintTurnoFooter(List<byte[]> commands, JToken turno)
        {
            string footer = SafeGetValue(turno, "footer", "BIENVENIDO");
            commands.Add(ESC_ALIGN_CENTER);
            commands.Add(ESC_BOLD_ON);
            commands.Add(ESC_MAX_HEIGHT);

            // Use wrapped text with appropriate width for the MAX_HEIGHT font size
            var wrappedLines = WrapText(footer, 24);
            foreach (var line in wrappedLines)
            {
                commands.Add(TextLine(line));
                commands.Add(LF);
            }

            commands.Add(ESC_NORMAL);
            commands.Add(ESC_BOLD_OFF);

            commands.Add(LF);
            AddDivider(commands);
            commands.Add(LF);
        }

        private void PrintLogoImage(List<byte[]> commands, string resourceName)
        {
            try
            {
                AppLogger.LogInfo($"Printing logo image from resource: {resourceName}");

                var config = new ThermalPrinterImageUtility.ImageConfig
                {
                    PrinterWidth = 384,
                    ProcessingMode = ThermalPrinterImageUtility.ImageProcessingMode.None,
                    ContrastThreshold = 128,
                    MaintainAspectRatio = true
                };

                var imageCommands = ThermalPrinterImageUtility.LoadEmbeddedImageAsCommands(resourceName, config);

                if (imageCommands != null && imageCommands.Count > 0)
                {
                    commands.Add(ESC_ALIGN_CENTER);
                    commands.AddRange(imageCommands);
                }
                else
                {
                    commands.Add(ESC_ALIGN_CENTER);
                    commands.Add(TextLine("Image not available"));
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error printing image: {ex.Message}", ex);
                commands.Add(ESC_ALIGN_CENTER);
                commands.Add(TextLine("Error printing image"));
                commands.Add(LF);
            }
        }
    }
}
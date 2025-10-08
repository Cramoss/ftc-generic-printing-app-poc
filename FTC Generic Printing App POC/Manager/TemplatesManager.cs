using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTC_Generic_Printing_App_POC.Interfaces;
using FTC_Generic_Printing_App_POC.Templates;
using FTC_Generic_Printing_App_POC.Utils;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Manager
{
    public class TemplatesManager
    {
        private readonly Dictionary<string, IDocumentTemplate> templates;

        public TemplatesManager()
        {
            // Document template registration. New templates can be added here.
            templates = new Dictionary<string, IDocumentTemplate>(StringComparer.OrdinalIgnoreCase)
            {
                { "test", new TestDocumentTemplate() },
                { "porcentaje", new PorcentajeDocumentTemplate() },
                { "cleveron", new CleveronDocumentTemplate() }
            };

            AppLogger.LogInfo($"DocumentTemplateManager initialized with {templates.Count} templates: ({string.Join(", ", templates.Keys)})");
        }

        public async Task<List<byte[]>> ProcessDocumentAsync(JObject document)
        {
            try
            {
                string templateId = document["id_plantilla"]?.ToString() ?? "default";

                if (string.IsNullOrEmpty(templateId))
                {
                    AppLogger.LogWarning("No template ID specified in document");
                    templateId = "default";
                }

                if (templates.TryGetValue(templateId.ToLower(), out var template))
                {
                    AppLogger.LogInfo($"Using template with ID: {templateId}");
                    return template.GenerateDocumentCommands(document);
                }

                AppLogger.LogWarning($"Template with ID '{templateId}' not found. Using default template.");

                if (templates.TryGetValue("default", out var defaultTemplate))
                {
                    return defaultTemplate.GenerateDocumentCommands(document);
                }

                throw new KeyNotFoundException($"No template found for ID: {templateId} and no default template available");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error processing document", ex);
                throw;
            }
        }
    }
}
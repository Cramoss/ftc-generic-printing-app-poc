using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTC_Generic_Printing_App_POC.Templates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Services
{
    public class DocumentTemplateManager
    {
        private readonly Dictionary<string, IDocumentTemplate> templates;

        public DocumentTemplateManager()
        {
            // Document template registration. New templates can be added here.
            templates = new Dictionary<string, IDocumentTemplate>(StringComparer.OrdinalIgnoreCase)
            {
                { "test", new TestDocumentTemplate() },
                { "porcentaje", new PorcentajeDocumentTemplate() }
            };

            AppLogger.LogInfo($"DocumentTemplateManager initialized with {templates.Count} templates");
        }

        public async Task<List<byte[]>> ProcessDocumentAsync(dynamic document)
        {
            try
            {
                AppLogger.LogInfo("Processing document from parsed data");

                // Get template type from the document data
                string templateType = document?.template?.ToString() ?? "test";
                AppLogger.LogInfo($"Detected document template type: {templateType}");

                if (!templates.TryGetValue(templateType, out var template))
                {
                    AppLogger.LogError($"Template '{templateType}' not found. Unable to process document");
                    return new List<byte[]>();
                }

                var commands = template.GenerateDocumentCommands(document);

                AppLogger.LogInfo($"Document processed successfully with template: {template.TemplateId}");
                return commands;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error processing document data", ex);
                throw;
            }
        }
    }
}
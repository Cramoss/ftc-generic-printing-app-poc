using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTC_Generic_Printing_App_POC.Templates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Services
{
    public class TicketTemplateManager
    {
        private readonly Dictionary<string, ITicketTemplate> templates;

        public TicketTemplateManager()
        {
            // Ticket template registration. New templates can be added here.
            templates = new Dictionary<string, ITicketTemplate>(StringComparer.OrdinalIgnoreCase)
            {
                { "test", new TestTicketTemplate() },
                { "order", new PorcentajeTicketTemplate() }
            };

            AppLogger.LogInfo($"TicketTemplateManager initialized with {templates.Count} templates");
        }

        // Process a ticket JSON string and return POS commands for the appropriate template
        public async Task<List<byte[]>> ProcessTicketAsync(string ticketJson)
        {
            try
            {
                AppLogger.LogInfo("Processing ticket and JSON deserialization");

                dynamic ticketData = JsonConvert.DeserializeObject<dynamic>(ticketJson);

                // Get template type from the ticket data
                string templateType = ticketData?.template?.ToString() ?? "test";
                AppLogger.LogInfo($"Detected Ticket template type: {templateType}");

                if (!templates.TryGetValue(templateType, out var template))
                {
                    AppLogger.LogError($"Template '{templateType}' not found. Unable to process ticket.");
                    return new List<byte[]>();
                }

                var commands = template.GenerateTicketCommands(ticketData);
                
                AppLogger.LogInfo($"Ticket processed successfully with template: {template.TemplateId}");
                return commands;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error processing ticket", ex);
                throw;
            }
        }
    }
}
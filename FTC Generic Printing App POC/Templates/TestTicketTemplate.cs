using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // Test ticket template for verifying printer functionality
    public class TestTicketTemplate : BaseTicketTemplate
    {
        public override string TemplateId => "test";

        public override List<byte[]> GenerateTicketCommands(dynamic ticketData)
        {
            var commands = new List<byte[]>();
            var config = ConfigurationManager.LoadConfiguration();

            // Initialize printer
            commands.Add(ESC_INIT);

            // Header
            AddHeaderText(commands, "TEST TICKET");
            commands.Add(LF);

            // Date info
            AddText(commands, $"Fecha: {DateTime.Now}");
            AddText(commands, "FTC Generic Printing App POC");
            commands.Add(LF);

            // Configuration info
            if (!string.IsNullOrEmpty(config.IdTotem))
            {
                AddText(commands, "Totem configurado");
                AddText(commands, $"Totem ID: {config.IdTotem}");
                AddText(commands, $"Store: {config.Store} ({config.StoreId})");
                AddText(commands, $"Country: {config.Country}");
                AddText(commands, $"Business: {config.Business}");
                commands.Add(LF);
            }

            // Test message
            AddText(commands, "Esto es un Ticket de test");
            AddText(commands, "Si puedes leer esto,");
            AddText(commands, "es porque la impresora está funcionando correctamente.");
            commands.Add(LF);

            // Footer
            AddDivider(commands);
            commands.Add(LF);

            // Cut paper
            commands.Add(ESC_CUT);

            return commands;
        }
    }
}

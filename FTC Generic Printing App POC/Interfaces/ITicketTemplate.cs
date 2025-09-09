using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // Interface defining the contract for all ticket templates
    public interface ITicketTemplate
    {
        string TemplateId { get; }

        // Generate ESC/POS commands for printing based on ticket data
        List<byte[]> GenerateTicketCommands(dynamic ticketData);
    }
}

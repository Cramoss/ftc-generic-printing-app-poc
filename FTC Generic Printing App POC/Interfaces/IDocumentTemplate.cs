using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Templates
{
    // Interface defining the contract for all document templates
    public interface IDocumentTemplate
    {
        string TemplateId { get; }

        // Generate ESC/POS commands for printing based on document data
        List<byte[]> GenerateDocumentCommands(JObject document);
    }
}

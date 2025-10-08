namespace FTC_Generic_Printing_App_POC.Models
{
    // Maybe this could be defined as StoreSettings as well
    public class Store
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string buName { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}

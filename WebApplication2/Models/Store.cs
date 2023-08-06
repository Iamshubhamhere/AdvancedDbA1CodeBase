namespace WebApplication2.Models
{
    public class Store
    {
        public Guid StoreNumber { get; set; }

        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public Canadian Province { get; set; }
        public HashSet<LaptopStore> LaptopStores { get; set; } = new HashSet<LaptopStore>();
    }
    public enum Canadian
    {
        Alberta,
        BritishColumbia,
        Manitoba,
        NewfoundlandAndLabrador,
        NovaScotia,
        Ontario,
        PrinceEdwardIsland,
        Quebec,
        Saskatchewan,
        NorthwestTerritories,
        Nunavut,
        Yukon
    }
}

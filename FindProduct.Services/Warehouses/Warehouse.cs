namespace FindProduct.Services.Warehouses
{
    public class Warehouse : IWarehouse
    {
        public string Url { get ;set ; }
        public string QueryParameter {get;set;}
        public int DistanceKm { get; set; }
    }
}

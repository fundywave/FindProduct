using FindProduct.Data.Response;
using FindProduct.Services.Interfaces;
using System.Threading.Tasks;

namespace FindProduct.Services.Warehouses
{

    public class Warehouse : IWarehouse
    {
        private readonly IWarehouseHttpClient<ProductSearchResponse> _warehouseHttpClient;

        public Warehouse()
        {
            _warehouseHttpClient = new WarehouseHttpClient<ProductSearchResponse>();
        }

        public string Url { get ;set ; }
        public string QueryParameter {get;set;}
        public int DistanceKm { get; set; }

        public async Task<ProductSearchResponse> GetProductByName(string productName)
        {
            return await _warehouseHttpClient.GetAsync(Url, string.Concat(QueryParameter, productName));
        }
    }
}

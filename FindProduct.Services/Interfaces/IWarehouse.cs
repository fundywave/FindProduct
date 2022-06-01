using FindProduct.Data.Response;
using System.Threading.Tasks;

namespace FindProduct.Services.Warehouses
{
    public interface IWarehouse
    {
        string Url { get; set;}
        string QueryParameter { get;set; }
        int DistanceKm{get;set;}
        Task<ProductSearchResponse> GetProductByName(string productName);
    }
}

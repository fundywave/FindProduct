using FindProduct.Data.Response;
using System.Threading.Tasks;
using FindProduct.API;

namespace FindProduct.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<CustomApiResponse<ProductSearchResponse>> FindProduct(string productName);
        Task<ProductSearchResponse> GetProductByName(string productName,string url,string Parameter);


    }
}

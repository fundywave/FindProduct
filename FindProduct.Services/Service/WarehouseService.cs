using FindProduct.Data.Response;
using FindProduct.Services.Warehouses;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FindProduct.API;
using FindProduct.Services.Interfaces;

namespace FindProduct.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IDataProvider<IWarehouse> _dataProvider;

        public WarehouseService(IDataProvider<IWarehouse> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<CustomApiResponse<ProductSearchResponse>> FindProduct(string productName)
        {
            var message = string.Empty;
            var statusCode=HttpStatusCode.OK;
            var productResult = new ProductSearchResponse();
            if (string.IsNullOrEmpty(productName))
            {
                return new CustomApiResponse<ProductSearchResponse>()
                {
                    Result = new ProductSearchResponse(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid parameter!"
                };
            }
            var warehouses = _dataProvider.Get();
            foreach (var item in warehouses.OrderBy(x => x.DistanceKm))
            {
                productResult =  await Task.Run(()=>item.GetProductByName(productName));
                if (productResult?.Product == null)
                {
                    statusCode = HttpStatusCode.NotFound;
                    message = "Product not found!";
                    continue;
                }
                statusCode = HttpStatusCode.OK;
                message = "Found product!";
                break;
            }
            return new CustomApiResponse<ProductSearchResponse>()
            {
                Result = productResult,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}

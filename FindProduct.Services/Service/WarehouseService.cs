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
        private readonly IDataProvider<Warehouse> _dataProvider;
        private readonly IWarehouseHttpClient<ProductSearchResponse> _warehouseHttpClient;


        public WarehouseService(IDataProvider<Warehouse> dataProvider, IWarehouseHttpClient<ProductSearchResponse> warehouseHttpClient)
        {
            _dataProvider = dataProvider;
            _warehouseHttpClient = warehouseHttpClient;

        }

        public async Task<CustomApiResponse<ProductSearchResponse>> FindProduct(string productName)
        {
            var message = string.Empty;
            var statusCode = HttpStatusCode.OK;
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
            //list of warehouses
            var warehouses = _dataProvider.Get();

            foreach (var warehouse in warehouses.OrderBy(x => x.DistanceKm))
            {
                productResult = await Task.Run(() => GetProductByName(productName,warehouse.Url,warehouse.QueryParameter));
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

        public async Task<ProductSearchResponse> GetProductByName(string productName, string url, string Paramter)
        {
            return await _warehouseHttpClient.GetAsync(url, string.Concat(Paramter, productName));
        }
    }
}

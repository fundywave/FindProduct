using FindProduct.Services.Interfaces;
using FindProduct.Services.Warehouses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FindProduct.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public ProductController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }
        [HttpGet]
        public ActionResult Get(string productName)
        {
            var result = _warehouseService.FindProduct(productName).Result;
            return Content(JsonConvert.SerializeObject(result, new StringEnumConverter()));
        }
    }
}

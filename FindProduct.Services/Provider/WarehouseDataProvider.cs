using FindProduct.Services.Interfaces;
using System.Collections.Generic;
using FindProduct.Services.Warehouses;
using Microsoft.Extensions.Options;

namespace FindProduct.Services.Provider
{
    public class WarehouseDataProvider : IDataProvider<Warehouse>
    {
        private readonly List<Warehouse> _warehouses;

        public WarehouseDataProvider(IOptions<List<Warehouse>> configuration)
        {
            _warehouses = configuration.Value;
        }

        public IEnumerable<Warehouse> Get() => _warehouses;
    }
}

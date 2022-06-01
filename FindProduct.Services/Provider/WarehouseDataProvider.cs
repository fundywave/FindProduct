using System;
using FindProduct.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Design;
using FindProduct.Services.Warehouses;
using Microsoft.Extensions.Configuration;
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

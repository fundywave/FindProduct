using FindProduct.Data.Enums;
using System.Collections.Generic;

namespace FindProduct.Data.Models
{
    public class Product
    {
        public List<DeliveryMethods> DeliveryMethods { get; set; }

        public Product()
        {
            DeliveryMethods = new List<DeliveryMethods>();
        }

        public string Name { get; set; }
    }
}

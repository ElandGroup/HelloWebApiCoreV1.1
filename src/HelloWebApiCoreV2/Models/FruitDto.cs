using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWebApiCoreV2.Models
{
    public class FruitDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Code { get; set; }
        public string StoreCode { get; set; }
    }
}

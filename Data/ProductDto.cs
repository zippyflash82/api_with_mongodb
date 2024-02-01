using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_with_mongodb.Data
{
    public class ProductDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
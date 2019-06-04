using System.Collections.Generic;
using Products_And_Categories.Models;

namespace Products_And_Categories.Models
{
    public class AddProductToCategoryView
    {
        public Categories Categories { get; set; }
        public IEnumerable<Products> Products { get; set; }
    }
}
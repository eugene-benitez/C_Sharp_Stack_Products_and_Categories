using System.Collections.Generic;
using Products_And_Categories.Models;

namespace Products_And_Categories.Models
{
    public class AddCategoryToProductView
    {
        public IEnumerable<Categories> Categories { get; set; }
        public Products Products { get; set; }
    }
}
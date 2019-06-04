using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace Products_And_Categories.Models
{
    public class Associations
    {
        [Key]
        public int AssociationId { get; set; }

        public int ProductId { get; set; }

        public int CategoriesId { get; set; }

        public Products Products { get; set; }

        public Categories Categories { get; set; }
    }
}
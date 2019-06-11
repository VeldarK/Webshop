using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webshop.Core.Models
{
    public class Product:BaseEntity
    {
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Range(typeof(decimal), "0", "1000")]
        public decimal Price { get; set; }

        public string Category { get; set; }
        public string Image { get; set; }
        
    }
}

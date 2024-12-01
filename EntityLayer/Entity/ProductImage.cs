using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public Product? Product { get; set; }
        public int ProductId { get; set; }  
    }
}

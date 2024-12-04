using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ResponseDto
{
    public class ResponseCategory
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
       // public bool IsActive { get; set; } = true;
       // public List<Product>? Product { get; set; }
    }
}

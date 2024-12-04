using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ResponseDto
{
    public class ResponseFavoriteProduct
    {
        public int FavoriteProductId { get; set; }
       // public User? User { get; set; }
        public int UserId { get; set; }
        // public Product? Product { get; set; }
        public int ProductId { get; set; }
       // public bool IsActive { get; set; } = true;
      //  [NotMapped]
       // public string? UserIdFromToken { get; set; }
    }
}

using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ResponseDto
{
    public class ResponseComment
    {
        public int CommentId { get; set; }
        public string? Feedback { get; set; }
       // public bool IsActive { get; set; } = true;
        public bool IfBuying { get; set; }
       // public Product? Product { get; set; }
        public int? ProductId { get; set; }
       // public User? User { get; set; }
        public int UserId { get; set; }
       // [NotMapped]
     //   public string? UserIdFromToken { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Feedback { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IfBuying { get; set; }
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class FeedbackScore
    {
        [Key]
        public int FeedbackScoreId { get; set; }
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
        public int CountStar { get; set; }
        public int AverageRating { get; set; }
        public bool IsActive { get; set; } = true;
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [NotMapped]
        public string? UserIdFromToken { get; set; }

    }
}

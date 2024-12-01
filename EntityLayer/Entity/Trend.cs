using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Trend
    {
        [Key]
        public int TrendId { get; set; }
        public DateTime TrendTime { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = true;
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}

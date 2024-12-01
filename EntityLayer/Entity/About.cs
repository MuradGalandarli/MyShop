using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        public string? Title { get; set; }
        public string? Topic { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; } = true;
        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}

using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DtoEntity
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int TotalCount { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }

        // [NotMapped]
        // public List<string>? ImageUrlUI { get; set; }
        // [NotMapped]
        public List<IFormFile>? ImageUI { get; set; }
        // public bool IsActive { get; set; } = true;
        // public Category? Category { get; set; }
        public int CategoryId { get; set; }
        //  public List<Comment>? Comments { get; set; }
        // public List<FavoriteProduct>? favoriteProducts { get; set; }
        // public List<FeedbackScore>? feedbackScores { get; set; }
        // public List<Order>? Orders { get; set; }
        // public List<ProductImage>? ProductImage { get; set; }
        // public List<Trend>? Trend { get; set; }
    }
}

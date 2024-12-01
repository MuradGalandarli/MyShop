using EntityLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationContext:IdentityDbContext
    {
        
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) 
        { 
        }
       public DbSet<Category>Categories { get; set; }
       public DbSet<Comment>Comments { get; set; }
       public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
       public DbSet<FeedbackScore> FeedbackScores { get; set; }
       public DbSet<Order>Orders { get; set; }
       public DbSet<Product>Products { get; set; }
       public DbSet<User>? Users { get; set; }
       public DbSet<ProductImage> ProductImages { get; set; }
       public DbSet<Trend>? Trends { get; set; }
       public DbSet<About>? Abouts { get; set; }
       public DbSet<Contact>Contacts { get; set; }




    }
}

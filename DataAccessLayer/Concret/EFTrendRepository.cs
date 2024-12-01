using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFTrendRepository : ITrend, ITrendPrtial
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFTrendRepository> _logger;
        public EFTrendRepository(ApplicationContext context,
            ILogger<EFTrendRepository> logger)
        {
            _context = context;
            _logger = logger;   
        }

        public async Task AddTrend(Trend trend)
        {
            try
            {
                await _context.Trends.AddAsync(trend);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               
            }
        }

        

        public async Task DeleteProductFromTrend(int ProductId)
        {
            try
            {
                var activeTrendOrder = await _context.Trends.Where(x => x.ProductId == ProductId && x.Status == true).ToListAsync();
                foreach (var trend in activeTrendOrder)
                {
                    trend.Status = false;

                }
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }

        public async Task<List<Product>> GetMostClicked()
        {
            try
            {
                var data = await _context.Trends.Where(x => x.Status == true).GroupBy(x => x.ProductId).
                    Select(e => new
                    {

                        productId = e.Key,
                        count = e.Count()

                    }).OrderByDescending(w => w.count).Select(x => x.productId).ToListAsync();

                var mostClicked = await _context.Products.Where(x => x.IsActive == true && data.Contains(x.ProductId)).
                    Include(a => a.ProductImage).Where(x => x.IsActive == true).Take(6).ToListAsync();

                return mostClicked;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<List<Product>> Trend()
        {
            try
            {
                var ProducrId = await _context.Trends.
                    Where(x => x.Status == true && x.TrendTime.AddMonths(1) > DateTime.UtcNow).GroupBy
                    (x => x.ProductId).Select(x => new
                    {
                        productId = x.Key,
                        count = x.Count()
                    }
                    ).OrderByDescending(x => x.count).Select(x => x.productId).ToListAsync();

                var favoriteAndProductId = await _context.FavoriteProducts.
                  Where(x => x.IsActive == true && ProducrId.Contains(x.ProductId)).GroupBy
                  (x => x.ProductId).Select(x => new
                  {
                      productId = x.Key,
                      count = x.Count()
                  }
                  ).OrderByDescending(x => x.count).Select(x => x.productId).ToListAsync();


                if (favoriteAndProductId.Count > 0)
                {
                    var product = await _context.Products.Where(x => x.IsActive == true && favoriteAndProductId.Contains(x.ProductId)).
                        Include(x => x.ProductImage.Where(x => x.IsActive == true)).Take(6).ToListAsync();

                    return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    
    }
}

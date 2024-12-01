using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFFavoriteProductRepository : IFavoriteProduct
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFFavoriteProductRepository> _logger;
        public EFFavoriteProductRepository(ApplicationContext context,
            ILogger<EFFavoriteProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Add(FavoriteProduct t)
        {
            try
            {
                var checkFavorite = await _context.FavoriteProducts.AnyAsync(x => x.ProductId == t.ProductId && x.UserId == t.UserId && x.IsActive == true);
                var checkUser = await _context.Users.AnyAsync(x => x.UserId == t.UserId);
                if (!checkFavorite && checkUser)
                {
                    await _context.FavoriteProducts.AddAsync(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data != null)
                {
                    data.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<List<FavoriteProduct>> GetAll()
        {
            try
            {
                var result = await _context.FavoriteProducts.Where(x => x.IsActive == true).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<FavoriteProduct> GetById(int id)
        {
            try
            {
                var result = await _context.FavoriteProducts.FirstOrDefaultAsync(x => x.IsActive == true && x.FavoriteProductId == id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Product>> GetUserIdAllFavoriteProduct(int userId)
        {
            try
            {
                var productId = await _context.FavoriteProducts.Where(x => x.IsActive == true && x.UserId == userId).Select(a => a.ProductId).ToListAsync();
                if (productId.Count > 0)
                {
                    var productImageList = await _context.Products.Where(x => x.IsActive == true && productId.Contains(x.ProductId)).Include(x => x.ProductImage).ToListAsync();
                    return productImageList;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /// //////////////  
        public async Task<bool> Update(FavoriteProduct t)
        {
            try
            {
                var checkFavorite = await _context.FavoriteProducts.AnyAsync(x => x.ProductId == t.ProductId && x.UserId == t.UserId && x.IsActive == true);
                var checkUser = await _context.Users.AnyAsync(x => x.UserId == t.UserId);
                if (!checkFavorite && checkUser)
                {
                    var result = _context.FavoriteProducts.Update(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}

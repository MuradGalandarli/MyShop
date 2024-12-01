using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{

    public class EFCategoryRepository : ICategory
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFCategoryRepository> _logger;
        public EFCategoryRepository(ApplicationContext context,
            ILogger<EFCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Add(Category t)
        {
            try
            {

                var checkCategory = await _context.Categories.AnyAsync(x => x.IsActive == true && x.CategoryName == t.CategoryName);
                if (!checkCategory)
                {
                    await _context.Categories.AddAsync(t);
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
                if (data.Product != null)
                {
                    var product = data.Product.Where(x => x.IsActive == true && x.CategoryId == data.CategoryId);
                    foreach (var item in product)
                    {
                        item.IsActive = false;
                        if (item.Comments != null)
                        {
                            foreach (var comment in item.Comments.Where(x => x.IsActive == true && x.ProductId == item.ProductId))
                            {
                                comment.IsActive = false;
                            }
                        }
                        if (item.favoriteProducts != null)
                        {
                            foreach (var favorite in item.favoriteProducts.Where(x => x.IsActive == true && x.ProductId == item.ProductId))
                            {
                                favorite.IsActive = false;
                            }
                        }
                        if (item.feedbackScores != null)
                        {
                            foreach (var feedback in item.feedbackScores.Where(x => x.IsActive == true && x.ProductId == item.ProductId))
                            {
                                feedback.IsActive = false;
                            }
                        }
                        if (item.Orders != null)
                        {
                            foreach (var ord in item.Orders.Where(x => x.OrderStatus == OrderEnum.OrderStatus.NotAvailable && x.ProductId == item.ProductId))
                            {
                                ord.OrderStatus = OrderEnum.OrderStatus.Discontinued;
                            }
                        }
                        if (item.ProductImage != null)
                        {
                            foreach (var img in item.ProductImage.Where(x => x.IsActive == true && x.ProductId == item.ProductId))
                            {
                                img.IsActive = false;
                            }
                        }
                    }
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


        public async Task<List<Category>> GetAll()
        {
            try
            {
                var result = await _context.Categories.Where(x => x.IsActive == true).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
           
        }


        public async Task<Category> GetById(int id)
        {
            try
            {
                var result = await _context.Categories.FirstOrDefaultAsync(x => x.IsActive == true && x.CategoryId == id);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
           
        }

        public async Task<List<Product>> GetByIdAllProduct(int id)
        {
            try
            {
                var data = await _context.Products.Where(x => x.CategoryId == id && x.IsActive == true).
             Include(x => x.ProductImage.Where(a => a.IsActive == true)).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
         
        }

        public async Task<bool> Update(Category t)
        {
            try
            {
                bool checkCategory = await _context.Categories.AnyAsync(x => x.IsActive == true && x.CategoryName == t.CategoryName);
                if (!checkCategory)
                {
                    var result = _context.Categories.Update(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return false;
            }

           
        }
    }
}

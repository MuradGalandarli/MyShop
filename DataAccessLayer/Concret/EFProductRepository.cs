using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFProductRepository : IProduct
    {

        private readonly ApplicationContext _context;
        private readonly ITrendPrtial _trendPrtial;
        private readonly ILogger<EFProductRepository> _logger;
        public EFProductRepository(ApplicationContext context,
            ITrendPrtial trendPrtial,
            ILogger<EFProductRepository> logger)
        {
            _context = context;
            _trendPrtial = trendPrtial;
            _logger = logger;
        }

        public async Task<bool> Add(Product t)
        {
            try
            {
                const string path = @"D:\MartImage";
                bool checkCategory = _context.Categories.Any(x => x.IsActive == true && x.CategoryId == t.CategoryId);
                if (checkCategory)
                {
                    var a = await _context.Products.AddAsync(new Product
                    {
                        ProductName = t.ProductName,
                        Brand = t.Brand,
                        Description = t.Description,
                        Price = t.Price,
                        CategoryId = t.CategoryId,
                        TotalCount = t.TotalCount

                    });

                    await _context.SaveChangesAsync();

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (t.ImageUI != null)
                    {
                        var productId = await _context.Products.OrderBy(x => x.ProductId).LastAsync();
                        foreach (var image in t.ImageUI)
                        {
                            string extension = Path.GetExtension(image.FileName);
                            string uniqueFileName = $"{Guid.NewGuid()}{extension}";

                            string fullPath = Path.Combine(path, uniqueFileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                            {
                                await image.CopyToAsync(stream);
                            }

                            _context.ProductImages.Add(new ProductImage
                            {
                                ImageUrl = fullPath,
                                ProductId = productId.ProductId,
                                IsActive = true

                            });

                            await _context.SaveChangesAsync();
                        }
                    }
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
                    if (data.Comments != null)
                    {
                        foreach (var item in data.Comments.Where(x => x.IsActive == true && x.ProductId == data.ProductId))
                        {
                            item.IsActive = false;
                        }
                    }
                    if (data.feedbackScores != null)
                    {
                        foreach (var feedback in data.feedbackScores.Where(x => x.IsActive == true && x.ProductId == data.ProductId))
                        {
                            feedback.IsActive = false;
                        }
                    }

                    if (data.favoriteProducts != null)
                    {
                        foreach (var favorite in data.favoriteProducts.Where(x => x.IsActive == true && x.ProductId == data.ProductId))
                        {
                            favorite.IsActive = false;
                        }
                    }
                    if (data.ProductImage != null)
                    {
                        foreach (var image in data.ProductImage.Where(x => x.IsActive == true && x.ProductId == data.ProductId))
                        {
                            image.IsActive = false;
                        }
                        if (data.Trend != null)
                        {
                            foreach (var image in data.Trend.Where(x => x.Status == true && x.ProductId == data.ProductId))
                            {
                                image.Status = false;
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

        public async Task<List<Product>> GetAll()
        {
            try
            {
                var result = await _context.Products.Where(x => x.IsActive == true).Include(x => x.ProductImage.Where(x => x.IsActive == true)).ToListAsync();
                return (result != null ? result : null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<Product> GetById(int id)
        {
            try
            {
                var result = await _context.Products.Where(x => x.IsActive == true)
     .Include(x => x.ProductImage.Where(x => x.IsActive == true)).
     FirstOrDefaultAsync(x => x.IsActive == true && x.ProductId == id);
                if (result != null)
                {
                    result.ImageUrlUI = result.ProductImage.Select(x => x.ImageUrl).ToList();

                    return result;
                }
                return (result != null ? result : null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<List<Product>> MostRecentlyUploaded()
        {
            try
            {
                var uploaded = await _context.Products.Where(x => x.IsActive == true).
                     Include(x => x.ProductImage.Where(a => a.IsActive == true)).
                     Select(x => new
                     {
                         productId = x.ProductId
                     }).OrderByDescending(x => x.productId).Select(i => i.productId).Take(6).ToListAsync();

                var product = await _context.Products.Where(x => uploaded.Contains(x.ProductId)).
                Include(x => x.ProductImage.Where(a => a.IsActive == true)).ToListAsync();

                return product != null ? product : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Product>> SearchProduct(SearchProductModel product)
        {
            try
            {
                var query = _context.Products.Include(x => x.ProductImage).AsQueryable();
                if (!string.IsNullOrEmpty(product.ProductName))
                {
                    query = query.Where(x => x.ProductName == product.ProductName && x.IsActive == true);
                }
                if (product.StartPrice > 0 && product.EndPrice > 0)
                {
                    query = query.Where(x => x.Price >= product.StartPrice && x.Price <= product.EndPrice && x.IsActive == true);
                }
                if (product.StartPrice > 0)
                {
                    query = query.Where(x => x.Price >= product.StartPrice && x.IsActive == true);
                }
                if (product.EndPrice > 0)
                {
                    query = query.Where(x => x.Price <= product.EndPrice && x.IsActive == true);
                }
                if (!string.IsNullOrEmpty(product.Brand))
                {
                    query = query.Where(x => x.Brand == product.Brand && x.IsActive == true);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public void DeleteImageFromFIle(string url)
        {
            try
            {
                var checkDirectory = Path.GetDirectoryName(url);
                var checkFile = Path.GetFileName(url);

                if (Directory.Exists(checkDirectory) && File.Exists(url))
                {
                    File.Delete(url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }

        public async Task<bool> Update(Product updatedProduct)
        {
            try
            {
                var existingProduct = await GetById(updatedProduct.ProductId);

                if (existingProduct != null)
                {
                    existingProduct.ProductName = updatedProduct.ProductName;
                    existingProduct.ProductId = updatedProduct.ProductId;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.Brand = updatedProduct.Brand;
                    existingProduct.IsActive = updatedProduct.IsActive;
                    existingProduct.Description = updatedProduct.Description;

                    if (updatedProduct.ImageUrlUI != null)
                    {
                        List<string> responceUlrDelete = new List<string>();

                        foreach (var image in updatedProduct.ImageUrlUI)
                        {
                            if (updatedProduct.ImageUrlUI != null)
                            {

                                foreach (var removeImage in updatedProduct.ImageUrlUI)
                                {
                                    var isRemoveImage = existingProduct.ProductImage.Where(x => x.IsActive == true && x.ImageUrl == removeImage).FirstOrDefault();
                                    if (isRemoveImage != null)
                                    {
                                        isRemoveImage.IsActive = false;
                                        responceUlrDelete.Add(removeImage);
                                    }

                                    DeleteImageFromFIle(removeImage);
                                }

                            }
                        }
                        if (responceUlrDelete != null)
                        {
                            foreach (var remove in responceUlrDelete)
                            {
                                updatedProduct.ImageUrlUI.Remove(remove);
                            }
                        }
                    }

                    if (updatedProduct.ImageUI != null)
                    {
                        foreach (var newImage in updatedProduct.ImageUI)
                        {
                            const string path = @"D:\MartImage";

                            string extension = Path.GetExtension(newImage.FileName);
                            string uniquePath = $"{Guid.NewGuid()}{extension}";
                            string fullPath = Path.Combine(path, uniquePath);

                            var checkDirectory = Path.GetDirectoryName(fullPath);


                            if (!Directory.Exists(checkDirectory))
                            {
                                Directory.CreateDirectory(checkDirectory);
                            }

                            if (Directory.Exists(checkDirectory))
                            {

                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    await newImage.CopyToAsync(stream);
                                }
                            }

                            existingProduct.ProductImage.Add(new ProductImage
                            {
                                ImageUrl = fullPath,
                                ProductId = existingProduct.ProductId,
                                IsActive = true
                            });

                        }
                    }

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

        public async Task<Product> GetByIdProductUI(int id)
        {
            try
            {
                var result = await _context.Products.Where(x => x.IsActive == true)
     .Include(x => x.ProductImage.Where(x => x.IsActive == true)).
     FirstOrDefaultAsync(x => x.IsActive == true && x.ProductId == id);
                if (result != null)
                {
                    result.ImageUrlUI = result.ProductImage.Select(x => x.ImageUrl).ToList();

                    await _trendPrtial.AddTrend(new Trend { ProductId = result.ProductId });
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}

using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class FavoriteProductManager:IFavoriteProductService
    {
        private readonly IFavoriteProduct _favoriteProduct;
        public FavoriteProductManager(IFavoriteProduct favoriteProduct)
        {
            _favoriteProduct = favoriteProduct;
        }

        public async Task<bool> Add(FavoriteProduct t)
        {
            bool IsSuccess = await _favoriteProduct.Add(t);
            return IsSuccess;
        }

        public async Task<bool> Delete(int id)
        {
            bool IsSuccess = await _favoriteProduct.Delete(id);
            return IsSuccess;
        }

        public async Task<List<FavoriteProduct>> GetAll()
        {
            var data = await _favoriteProduct.GetAll();
            return data;
        }

        public async Task<FavoriteProduct> GetById(int id)
        {
            var data = await _favoriteProduct.GetById(id);
            return data;
        }

        public async Task<List<Product>> GetUserIdAllFavoriteProduct(int userId)
        {

            var data = await _favoriteProduct.GetUserIdAllFavoriteProduct(userId);
            return data;
        }

        public async Task<bool> Update(FavoriteProduct t)
        {
            bool IsSuccess = await _favoriteProduct.Update(t);
            return IsSuccess;
        }
    }
}

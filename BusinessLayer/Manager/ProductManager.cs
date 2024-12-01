using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using SahredLayer;

namespace BusinessLayer.Manager
{
    public class ProductManager:IProductService
    {
        private readonly IProduct _product;
        public ProductManager(IProduct product)
        {
            _product = product;
        }

    

        public async Task<bool> Add(Product t)
        {
          bool data = await _product.Add(t);
            return data;
        }

        public async Task<bool> Delete(int id)
        {
            bool data = await _product.Delete(id);
            return data;
        }

        public async Task<List<Product>> GetAll()
        {
            var data = await _product.GetAll();
            return data;
        }

        public async Task<Product> GetById(int id)
        {
            var data = await _product.GetById(id);
            
            return data;
        }

        public async Task<Product> GetByIdProductUI(int id)
        {
          var data = await _product.GetByIdProductUI(id);
            return data;
        }

        public Task<List<Product>> MostRecentlyUploaded()
        {
           var data = _product.MostRecentlyUploaded();
            return data;
        }

        public Task<List<Product>> SearchProduct(SearchProductModel product)
        {
           var result = _product.SearchProduct(product);
            return result;
        }

        public async Task<bool> Update(Product t)
        {
            bool data = await _product.Update(t);
            return data;
        }
    }
}

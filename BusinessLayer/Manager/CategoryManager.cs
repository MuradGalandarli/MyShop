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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategory _category;
        public CategoryManager(ICategory category)
        {
            _category = category;
        }

        public async Task<bool> Add(Category t)
        {
            bool IsSuccess = await _category.Add(t);
            return IsSuccess;
        }

        public async Task<bool> Delete(int id)
        {
           bool IsSuccess = await _category.Delete(id);
            return IsSuccess;
        }

        public async Task<List<Category>> GetAll()
        {
            var data = await _category.GetAll();
            return data;
        }

        public async Task<Category> GetById(int id)
        {
            var data = await _category.GetById(id);
            return data;
        }

        public async Task<List<Product>> GetCategoryIdAllProduct(int id)
        {
            var result = await _category.GetByIdAllProduct(id);
            return result;
        }

        public async Task<bool> Update(Category t)
        {
            bool IsSuccess = await _category.Update(t);
            return IsSuccess;
        }
    }
}

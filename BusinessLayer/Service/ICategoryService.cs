using EntityLayer.Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ICategoryService:IGenericService<Category>
    {
        public Task<List<Product>> GetCategoryIdAllProduct(int id);
    }
}

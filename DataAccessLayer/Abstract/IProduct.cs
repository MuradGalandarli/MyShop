using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProduct:IGeneric<Product>
    {
        public Task<List<Product>> SearchProduct(SearchProductModel product);
        public Task<List<Product>> MostRecentlyUploaded();
        public Task<Product> GetByIdProductUI(int id);

    }
}

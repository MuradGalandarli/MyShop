using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IFavoriteProductService:IGenericService<FavoriteProduct>
    {
        public Task<List<Product>> GetUserIdAllFavoriteProduct(int userId);
    }
}

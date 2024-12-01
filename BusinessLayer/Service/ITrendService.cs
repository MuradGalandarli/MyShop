using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
   public interface ITrendService
    {
        public Task DeleteProductFromTrend(int ProductId);
        public Task<List<Product>> Trend();
        public Task<List<Product>> GetMostClicked();
    }
}

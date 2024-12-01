using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITrend
    {
        public Task DeleteProductFromTrend(int ProductId);
        public Task<List<Product>> GetMostClicked();
        public Task<List<Product>> Trend();

    }
}

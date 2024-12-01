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
    public class TrendManager : ITrendService
    {
        private readonly ITrend _trend;

        public TrendManager(ITrend _trend)
        {
            this._trend = _trend; 
        }
       

        public async Task DeleteProductFromTrend(int ProductId)
        {
           await _trend.DeleteProductFromTrend(ProductId);    
        }

        public async Task<List<Product>> GetMostClicked()
        {
            var data = await _trend.GetMostClicked();
            return data;
        }

        public async Task<List<Product>> Trend()
        {
            var data = await _trend.Trend();
            return data;
        }
    }
}

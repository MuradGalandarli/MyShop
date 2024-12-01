using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using EntityLayer.Enums;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class OrderManager:IOrderService
    {
        private readonly IOrder _order;
        public OrderManager(IOrder order)
        {
           _order = order;
        }

        public Task<bool> Add(Order t)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderEnum.OrderStatus> AddOrder(Order o)
        {
            var data = await _order.AddOrder(o);
            return data;
        }

        public async Task<List<Product>> BestSeller()
        {
            var data = await _order.BestSeller();
            return data;
        }

        public async Task<OrderEnum.OrderStatus> Cancellation(int orderId)
        {
            var data = await _order.Cancellation(orderId);
            return data;
        }

        public async Task<bool> Delete(int id)
        {
            bool data = await _order.Delete(id);
            return data;
        }

        public async Task<List<Order>> GetAll()
        {
            var data = await _order.GetAll();
            return data;
        }

        public async Task<Order> GetById(int id)
        {
            var data = await _order.GetById(id);
            return data;
        }

        public async Task<Order> GetByIdAddedToCartOrder(int orderId)
        {
            var data = await _order.GetByIdAddedToCartOrder(orderId);
            return data;
        }

        public async Task<List<Order>> OrderAddedToCartAllListOrder()
        {
            var data = await _order.OrderAddedToCartAllListOrder();
            return data;
        }

        public async Task<OrderEnum.OrderStatus> Selled(int orderId)
        {
            var data = await _order.Selled(orderId);
            return data;
        }

        public async Task<bool> Update(Order t)
        {
            bool data = await _order.Update(t);
            return data;
        }
    }
}

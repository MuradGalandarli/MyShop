﻿using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EntityLayer.Enums.OrderEnum;

namespace BusinessLayer.Service
{
    public interface IOrderService:IGenericService<Order>
    {
        public Task<OrderStatus> Selled(int orderId);
        public Task<OrderStatus> Cancellation(int orderId);
        public Task<OrderStatus> AddOrder(Order o);
        public Task<List<Order>> OrderAddedToCartAllListOrder();
        public Task<Order> GetByIdAddedToCartOrder(int orderId);
        public Task<List<Product>> BestSeller();
    }
}

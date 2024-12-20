﻿using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DtoEntity
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public byte OrderCount { get; set; }
        public decimal TotalAmount { get; set; }
        // public DateTime SellTime { get; set; }
        // public Enums.OrderEnum.OrderStatus OrderStatus { get; set; }
        //  public User? User { get; set; }
        //public int UserId { get; set; }
        // public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}


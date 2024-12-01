using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using EntityLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EntityLayer.Enums.OrderEnum;

namespace DataAccessLayer.Concret
{
    public class EFOrderRepository : IOrder
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFOrderRepository> _logger;
        public EFOrderRepository(ApplicationContext context,
            ILogger<EFOrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<bool> Add(Order t)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderStatus> AddOrder(Order o)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == o.ProductId && x.IsActive == true);
                var productDelete = _context.Products.Any(x => x.ProductId == o.ProductId && x.IsActive == false);

                if (product == null)
                {
                    await _context.Orders.AddAsync(o);
                    o.OrderStatus = OrderEnum.OrderStatus.Discontinued;
                    await _context.SaveChangesAsync();
                    return OrderStatus.Discontinued;
                }

                if (productDelete)
                {
                    o.OrderStatus = OrderEnum.OrderStatus.Discontinued;
                    await _context.Orders.AddAsync(o);
                    await _context.SaveChangesAsync();
                    return OrderEnum.OrderStatus.Discontinued;
                }
                if (product != null && o.OrderCount >= 1)
                {
                    if (product.TotalCount - o.OrderCount < 0)
                    {
                        o.OrderStatus = OrderEnum.OrderStatus.OutOfStock;
                        await _context.Orders.AddAsync(o);
                        await _context.SaveChangesAsync();
                        return OrderEnum.OrderStatus.OutOfStock;
                    }

                    if (product.Price != o.TotalAmount)
                    {
                        await _context.Orders.AddAsync(o);
                        o.OrderStatus = OrderEnum.OrderStatus.BuyError;
                        await _context.SaveChangesAsync();
                        return OrderStatus.AddedToCart;
                    }
                    if (product.Price == o.TotalAmount)
                    {
                        await _context.Orders.AddAsync(o);
                        o.OrderStatus = OrderEnum.OrderStatus.AddedToCart;
                        await _context.SaveChangesAsync();
                        return OrderStatus.AddedToCart;
                    }
                }
                return OrderStatus.Error;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return OrderStatus.Error;
            }

        }

        public async Task<List<Product>> BestSeller()
        {
            try
            {
                var seledProduct1 = await _context.Orders.
                    Where(x => x.OrderStatus == OrderStatus.WasSold && x.SellTime.AddMonths(1) > DateTime.UtcNow
                  ).

                    GroupBy(x => x.Product).Select(x => new
                    {
                        count = x.Count(),
                        productId = x.Key.ProductId

                    }).OrderByDescending(x => x.productId).Select(x => x.productId).ToListAsync();

                var bestSellerProduct = await _context.Products.
                       Where(x => seledProduct1.Contains(x.ProductId) && x.IsActive == true).
                       Include(x => x.ProductImage.Where(x => x.IsActive == true)).Take(6).ToListAsync();

                return bestSellerProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<OrderEnum.OrderStatus> Cancellation(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId && x.OrderStatus == OrderEnum.OrderStatus.AddedToCart);
                if (order != null)
                {
                    order.OrderStatus = OrderEnum.OrderStatus.Cancellation;
                    await _context.SaveChangesAsync();
                    return OrderEnum.OrderStatus.Cancellation;
                }
                return OrderEnum.OrderStatus.NotAddedToCart;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return OrderStatus.Error;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data != null)
                {
                    data.OrderStatus = OrderEnum.OrderStatus.Delete;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<List<Order>> GetAll()
        {
            try
            {
                var result = await _context.Orders.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<Order> GetById(int id)
        {
            try
            {
                var result = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id && x.OrderStatus == OrderEnum.OrderStatus.AddedToCart);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Order>> OrderAddedToCartAllListOrder()
        {
            try
            {
                var data = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.AddedToCart).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Order> GetByIdAddedToCartOrder(int orderId)
        {
            try
            {
                var data = await _context.Orders.FirstOrDefaultAsync(x => x.OrderStatus == OrderStatus.AddedToCart && x.OrderId == orderId);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /*  InStock = 1,      // Ürün stokta mevcut
   OutOfStock,       // Ürün stokta yok **********
   AddedToCart,      // Ürün sepete eklendi **********
   Discontinued,     // Ürün artık satışta değil ********
   Delete,            // Sifaris silindi ******* 
   WasSold           // Satildi
   cancellation       legv
    NotAvailable                 Mövcud deyil
        BuyError        satın alma xətası
        Error           gozlenilmeyen xeta
     NotAddedToCart    Sebete elave edilmeyib
    
         */

        public async Task<OrderStatus> Selled(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId && x.OrderStatus == OrderEnum.OrderStatus.AddedToCart);
                if (order != null)
                {
                    var product = _context.Products.FirstOrDefault(x => x.ProductId == order.ProductId && x.IsActive == true);
                    if (product != null)
                    {
                        if (product.TotalCount <= 0)
                        {
                            product.IsActive = false;
                            order.OrderStatus = OrderStatus.Discontinued;
                            await _context.SaveChangesAsync();
                            return OrderStatus.Discontinued;
                        }

                        if ((product.TotalCount - order.OrderCount) < 0)
                        {

                            order.OrderStatus = OrderStatus.OutOfStock;
                            await _context.SaveChangesAsync();
                            return OrderStatus.OutOfStock;
                        }



                        order.SellTime = DateTime.UtcNow;
                        order.OrderStatus = OrderStatus.WasSold;
                        product.TotalCount -= order.OrderCount;
                        await _context.SaveChangesAsync();
                        return OrderStatus.WasSold;
                    }
                    if (product == null)
                    {
                        order.OrderStatus = OrderStatus.Discontinued;
                        await _context.SaveChangesAsync();
                        return OrderStatus.Discontinued;
                    }

                }

                return OrderStatus.NotAddedToCart;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return OrderStatus.Error;
            }
        }

        public async Task<bool> Update(Order t)
        {
            try
            {
                var result = _context.Orders.Update(t);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}

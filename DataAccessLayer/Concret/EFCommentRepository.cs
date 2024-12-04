using DataAccessLayer.Abstract;
using EntityLayer.Entity;
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
    public class EFCommentRepository : IComment
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFCommentRepository> _logger;
        public EFCommentRepository(ApplicationContext context,
            ILogger<EFCommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Add(Comment t)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.ApplicationUserId == t.UserIdFromToken);
                if (user != null)
                {
                    t.UserId = user.UserId;
                }
                var checkUser = _context.Users.Any(x => x.UserId == t.UserId);
                var checkProduct = _context.Products.Any(x => x.ProductId == t.ProductId && x.IsActive == true);
                var checkOrderStatus = _context.Orders.Any(x => x.UserId == t.UserId && x.ProductId == t.ProductId && x.OrderStatus == OrderStatus.WasSold);
                if (checkUser && checkProduct)
                {
                    if (checkOrderStatus)
                    {
                        t.IfBuying = true;
                    }
                    await _context.Comments.AddAsync(t);
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                var checkUser = _context.Users.Any(x => x.UserId == data.UserId);
                var checkProduct = _context.Products.Any(x => x.ProductId == data.ProductId && x.IsActive == true);
                if (checkUser && checkProduct)
                {
                    data.IsActive = false;
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


        public async Task<List<Comment>> GetAll()
        {
            try
            {
                var result = await _context.Comments.Where(x => x.IsActive == true).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<Comment> GetById(int id)
        {
            try
            {
                var result = await _context.Comments.FirstOrDefaultAsync(x => x.IsActive == true && x.CommentId == id);
                if (result != null)
                {
                    var checkUser = _context.Users.Any(x => x.UserId == result.UserId);
                    var checkProduct = _context.Products.Any(x => x.ProductId == result.ProductId && x.IsActive == true);

                    if (checkUser && checkProduct)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Comment>> GetByProductIdAllComment(int productId)
        {
            var data = await _context.Comments.Where(x => x.ProductId == productId && x.IsActive == true).ToListAsync();
            if(data != null)
            {
                return data;
            }
            return new List<Comment>();
        }

        public async Task<bool> Update(Comment t)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.ApplicationUserId == t.UserIdFromToken);
                if (user != null)
                {
                    t.UserId = user.UserId;
                }
                var checkUser = _context.Users.Any(x => x.UserId == t.UserId);
                var checkProduct = _context.Products.Any(x => x.ProductId == t.ProductId && x.IsActive == true);
                var checkComment = _context.Comments.Any(x => x.CommentId == t.CommentId && x.IsActive == true);
                if (checkUser && checkProduct && checkComment)
                {
                    var result = _context.Comments.Update(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }
    }
}

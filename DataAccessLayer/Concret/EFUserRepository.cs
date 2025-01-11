using Azure.Core;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFUserRepository : IUserApp
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EFUserRepository> _logger;
        public EFUserRepository(ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<EFUserRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Add(User t)
        {
            try
            {
                var checkUserId = await _userManager.FindByIdAsync(t.ApplicationUserId);

                bool checkUser = _context.Users.Any(x => x.ApplicationUserId == t.ApplicationUserId);

                if (checkUserId != null && checkUserId.Id == t.ApplicationUserId && !checkUser)
                {
                    await _context.Users.AddAsync(t);
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
                if (data != null)
                {
                    var checkUser = await _userManager.FindByIdAsync(data.ApplicationUserId);

                    if (checkUser != null)
                    {
                        _context.Users.Remove(data);

                        if (data.Orders != null)
                        {
                            foreach (var order in data.Orders.Where(x => x.OrderStatus == EntityLayer.Enums.OrderEnum.OrderStatus.AddedToCart))
                            {
                                order.OrderStatus = EntityLayer.Enums.OrderEnum.OrderStatus.Delete;
                            }
                        }

                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUserWithToken(string userId)
        {
            try
            {
                var data = await _context.Users.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
                if (data != null)
                {
                    var checkUser = await _userManager.FindByIdAsync(data.ApplicationUserId);

                    if (checkUser != null)
                    {
                        _context.Users.Remove(data);

                        if (data.Orders != null)
                        {
                            foreach (var order in data.Orders.Where(x => x.OrderStatus == EntityLayer.Enums.OrderEnum.OrderStatus.AddedToCart))
                            {
                                order.OrderStatus = EntityLayer.Enums.OrderEnum.OrderStatus.Delete;
                            }
                        }

                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                var result = await _context.Users.ToListAsync();
                return (result != null ? result : null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<User> GetById(int id)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                return (result != null ? result : null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetByIdWithToken(string userId)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(User t)
        {
            try
            {
                var checkUserId = await _userManager.FindByIdAsync(t.ApplicationUserId);

                bool checkUser = false;
                if (checkUserId != null)
                {
                    checkUser = _context.Users.Any(x => x.ApplicationUserId == t.ApplicationUserId && x.UserId == t.UserId);
                }
                if (checkUser)
                {
                    var result = _context.Users.Update(t);
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
    }
}

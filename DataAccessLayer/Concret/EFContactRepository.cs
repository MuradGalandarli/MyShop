using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFContactRepository : IContact
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFContactRepository> _logger;
        public EFContactRepository(ApplicationContext _context,
            ILogger<EFContactRepository> logger)
        {
            this._context = _context;
            _logger = logger;
        }
        public async Task<bool> Add(Contact t)
        {
            try
            {
                if (t != null)
                {
                    await _context.AddAsync(t);
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

        public async Task<List<Contact>> GetAll()
        {
            try
            {
                var data = await _context.Contacts.Where(x => x.IsActive == true).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Contact> GetById(int id)
        {
            try
            {
                var data = await _context.Contacts.FirstOrDefaultAsync(x => x.IsActive == true && x.ContactId == id);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(Contact t)
        {
            try
            {
                _context.Contacts.Update(t);
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

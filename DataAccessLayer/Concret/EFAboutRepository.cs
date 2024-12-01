using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFAboutRepository : IAbout
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFAboutRepository> _logger;
        public EFAboutRepository(ApplicationContext context,
             ILogger<EFAboutRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Add(About t)
        {
            try
            {
                if (t.Image != null)
                {
                    const string path = "C:\\AboutImage/";
                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var extension = Path.GetExtension(t.Image.FileName);

                    var fullPath = Path.Combine(path, $"{Guid.NewGuid()}{extension}");

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await t.Image.CopyToAsync(stream);
                    };

                    t.ImageUrl = fullPath;
                }

                await _context.AddAsync(t);
                await _context.SaveChangesAsync();
                return true;
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
                    _context.Remove(data);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return false;
            }
            return false;
        }

        public async Task<List<About>> GetAll()
        {
            try
            {
                var data = await _context.Abouts.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
        }

        public async Task<About> GetById(int id)
        {
            try
            {
                var data = await _context.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
                return data;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }

        }

        public async Task<List<About>> GetListAllIsActiveUI()
        {
            try
            {
                var data = await _context.Abouts.Where(x => x.Status == true).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
        }

        public async Task IsActive(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data != null)
                {
                    if (data.Status)
                    {
                        data.Status = false;
                    }
                    else
                    {
                        data.Status = true;
                    }
                    await _context.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }

        public async Task<bool> Update(About t)
        {
            try
            {

                var updateAbout = await GetById(t.AboutId);
                if (updateAbout == null)
                {
                    return false;
                }
                const string path = "C:\\AboutImage/";

                if (t.Image != null)
                {
                    string checkPath = Path.GetFileName(updateAbout.ImageUrl);
                    if (!Path.Exists(checkPath))
                    {
                        File.Delete(updateAbout.ImageUrl);
                    }

                    var extension = Path.GetExtension(t.Image.FileName);

                    var fullPath = Path.Combine(path, $"{Guid.NewGuid()}{extension}");

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await t.Image.CopyToAsync(stream);
                    };

                    updateAbout.ImageUrl = fullPath;
                }

                updateAbout.Topic = t.Topic;
                updateAbout.AboutId = t.AboutId;
                updateAbout.Title = t.Title;
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


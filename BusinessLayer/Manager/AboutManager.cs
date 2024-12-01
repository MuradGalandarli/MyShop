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
    public class AboutManager : IAboutService
    {
        private readonly IAbout _about;
        public AboutManager(IAbout about)
        {
            _about = about;
        }
        public Task<bool> Add(About t)
        {
            var IsStatus = _about.Add(t);
            return IsStatus;
        }

        public Task<bool> Delete(int id)
        {
            var IsStatus = _about.Delete (id);
            return IsStatus;
        }

        public async Task<List<About>> GetAll()
        {
            var result = await _about.GetAll();
            return result;
        }

        public async Task<About> GetById(int id)
        {
            var result = await _about.GetById(id);
            return result;
        }

        public async Task<List<About>> GetListAllIsActiveUI()
        {
            var result = await _about.GetListAllIsActiveUI();
            return result;
        }

        public async Task IsActive(int id)
        {
           await _about.IsActive(id);
           
        }

        public Task<bool> Update(About t)
        {
            var IsStatus = _about.Update(t);
            return IsStatus;
        }
    }
}

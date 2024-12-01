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
    public class UserManager:IUserService
    {
        private readonly IUserApp _user;
        public UserManager(IUserApp user)
        {
            _user = user;
        }

        public async Task<bool> Add(User t)
        {
            bool data = await _user.Add(t);
            return data;
        }

        public async Task<bool> Delete(int id)
        {
            bool data = await _user.Delete(id);
            return data;
        }

        public async Task<List<User>> GetAll()
        {
            var data = await _user.GetAll();
            return data;
        }

        public async Task<User> GetById(int id)
        {
            var data = await _user.GetById(id);
            return data;
        }

        public async Task<bool> Update(User t)
        {
            bool data = await _user.Update(t);
            return data;
        }
    }
}

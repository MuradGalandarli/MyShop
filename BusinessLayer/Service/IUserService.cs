using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IUserService:IGenericService<User>
    {
        public Task<bool> DeleteUserWithToken(string userId);
        public Task<User> GetByIdWithToken(string userId);
    }
}

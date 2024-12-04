using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserApp:IGeneric<User>
    {
        public Task<bool> DeleteUserWithToken(string userId);
        public Task<User> GetByIdWithToken(string userId);
    }
}

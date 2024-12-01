using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IAboutService:IGenericService<About>
    {
        Task IsActive(int id);
        Task<List<About>> GetListAllIsActiveUI();
    }
}

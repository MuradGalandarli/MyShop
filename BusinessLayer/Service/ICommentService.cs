using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ICommentService:IGenericService<Comment>
    {
        public Task<List<Comment>> GetByProductIdAllComment(int productId);
    }
}

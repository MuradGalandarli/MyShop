﻿using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IComment:IGeneric<Comment>
    {
        public Task<List<Comment>> GetByProductIdAllComment(int productId);
    }
}

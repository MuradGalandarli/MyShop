using EntityLayer.Entity;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFeedbackScore:IGeneric<FeedbackScore>
    {
        public Task<FeedbackScore> Score(int id);
    }
}

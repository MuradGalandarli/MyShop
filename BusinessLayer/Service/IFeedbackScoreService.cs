using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IFeedbackScoreService:IGenericService<FeedbackScore>
    {
        public Task<FeedbackScore> Score(int id);
    }
}

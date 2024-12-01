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
    public class FeedbackScoreManager:IFeedbackScoreService
    {
        private readonly IFeedbackScore _feedbackScore;
        public FeedbackScoreManager(IFeedbackScore feedbackScore)
        {
            _feedbackScore = feedbackScore;
        }

        public async Task<bool> Add(FeedbackScore t)
        {
            bool data = await _feedbackScore.Add(t);
            return data;
        }

        public async Task<bool> Delete(int id)
        {
            bool data = await _feedbackScore.Delete(id);
            return data;
        }

        public async Task<List<FeedbackScore>> GetAll()
        {
            var data = await _feedbackScore.GetAll();
            return data;
        }

        public async Task<FeedbackScore> GetById(int id)
        {
            var data = await _feedbackScore.GetById(id);
            return data;
        }

        public async Task<FeedbackScore> Score(int id)
        {
            var data = await _feedbackScore.Score(id);
            return data;
        }

        public async Task<bool> Update(FeedbackScore t)
        {
            bool data = await _feedbackScore.Update(t);
            return data;
        }
    }
}

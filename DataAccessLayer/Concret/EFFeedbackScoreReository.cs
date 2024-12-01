using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret
{
    public class EFFeedbackScoreReository : IFeedbackScore
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFFeedbackScoreReository> _logger;
        public EFFeedbackScoreReository(ApplicationContext context,
           ILogger<EFFeedbackScoreReository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Add(FeedbackScore t)
        {
            try
            {
                var checkFeedbackScores = _context.FeedbackScores.Any(x => x.ProductId == t.ProductId && x.UserId == t.UserId && x.IsActive == true);
                var checkProduct = _context.Products.Any(x => x.ProductId == t.ProductId && x.IsActive == true);
                var checkUser = _context.Users.Any(x => x.UserId == t.UserId);
                if (!checkFeedbackScores && checkProduct && checkUser && t.CountStar > 0 && t.CountStar <= 5)
                {
                    switch (t.CountStar)
                    {
                        case 1: t.OneStar = 1; break;
                        case 2: t.TwoStar = 1; break;
                        case 3: t.ThreeStar = 1; break;
                        case 4: t.FourStar = 1; break;
                        case 5: t.FiveStar = 1; break;
                    }

                    await _context.FeedbackScores.AddAsync(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data != null)
                {
                    data.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<List<FeedbackScore>> GetAll()
        {
            try
            {
                var result = await _context.FeedbackScores.Where(x => x.IsActive == true).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<FeedbackScore> GetById(int id)
        {
            try
            {
                var result = await _context.FeedbackScores.FirstOrDefaultAsync(x => x.IsActive == true && x.FeedbackScoreId == id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<FeedbackScore> Score(int productId)
        {
            try
            {
                var checkProduct = _context.Products.Any(x => x.ProductId == productId && x.IsActive == true);
                if (checkProduct)
                {

                    var data = await _context.FeedbackScores.Where(x => x.ProductId == productId && x.IsActive == true).ToListAsync();
                    if (data != null)
                    {
                        int oneStar = data.Sum(x => x.OneStar);
                        int twoStar = data.Sum(x => x.TwoStar);
                        int threeStar = data.Sum(x => x.ThreeStar);
                        int fourStar = data.Sum(x => x.FourStar);
                        int fiveStar = data.Sum(x => x.FiveStar);
                        var result = (oneStar + (twoStar * 2) + (threeStar * 3) + (fourStar * 4) + (fiveStar * 5));
                        result = (result > 0 ? result / data.Count() : result);
                        return new FeedbackScore
                        {
                            OneStar = oneStar,
                            TwoStar = twoStar,
                            ThreeStar = threeStar,
                            FourStar = fourStar,
                            FiveStar = fiveStar,
                            AverageRating = result

                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(FeedbackScore t)
        {
            try
            {
                var result = _context.FeedbackScores.Update(t);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


    }
}

using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DtoEntity
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? SureName { get; set; }
        // public List<Order>? Orders { get; set; }
        //  public List<FeedbackScore>? FeedbackScores { get; set; }
        public string? ApplicationUserId { get; set; }
        // public ApplicationUser? ApplicationUser { get; set; }
        //  public List<Comment>? Comments { get; set; }
    }
}

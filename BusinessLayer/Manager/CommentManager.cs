using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class CommentManager:ICommentService
    {
        private readonly IComment _comment;
        public CommentManager(IComment comment)
        {
            _comment = comment;
        }

        public async Task<bool> Add(Comment t)
        {
            bool IsSuccess = await _comment.Add(t);
            return IsSuccess;
        }

        public async Task<bool> Delete(int id)
        {
            bool IsSuccess = await _comment.Delete(id);
            return IsSuccess;
        }

        public async Task<List<Comment>> GetAll()
        {
            var data = await _comment.GetAll();
            return data;
        }

        public async Task<Comment> GetById(int id)
        {
            var data = await _comment.GetById(id);
            return data;
        }

        public async Task<List<Comment>> GetByProductIdAllComment(int productId)
        {
            var data = await _comment.GetByProductIdAllComment(productId);
            return data;
        }

        public async Task<bool> Update(Comment t)
        {
            bool IsSuccess = await _comment.Update(t);
            return IsSuccess;
        }
    }
}

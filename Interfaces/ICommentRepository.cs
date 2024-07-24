using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject);

         Task<Comment?> GetByIdAsync(int id);  //GET NULL IF THERE IS NO NULL
        Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment?> UpdateCommentAsync(int id, Comment comment);

        Task<Comment?> DeleteCommentAsync(int id);
    }
}
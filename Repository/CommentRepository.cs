using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _applicationDBContext;

        public CommentRepository(ApplicationDBContext applicationDBContext){
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _applicationDBContext.Comments.AddAsync(comment);
            await _applicationDBContext.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _applicationDBContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null){
                return null;
            }
            _applicationDBContext.Comments.Remove(comment);
            await _applicationDBContext.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _applicationDBContext.Comments.Include(a =>a.appUser).ToListAsync();
        }

       

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Comments.Include(a =>a.appUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            var existingComment = await _applicationDBContext.Comments.FindAsync(id);

            if(existingComment == null){
                return  null;
            }

            existingComment.Title = comment.Title;

            existingComment.Content = comment.Content;

            await _applicationDBContext.SaveChangesAsync();

            return existingComment;
        }
    }
}


// BASE REPOSISTORY USING t (GENERIC)
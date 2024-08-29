using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> CommentExists(int id)
        {
            return _context.Comments.AnyAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteComment(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public Task<Comment?> GetCommentById(int id)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateComment(int id, CommentUpdateModel comment)
        {
            var commentUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentUpdate == null)
            {
                return null;
            }
            commentUpdate.Title = comment.Title;
            commentUpdate.Content = comment.Content;

            await _context.SaveChangesAsync();
            return commentUpdate;
        }
    }
}
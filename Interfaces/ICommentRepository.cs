using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        Task<Comment> CreateComment(Comment comment);
        Task<Comment?> UpdateComment(int id, CommentUpdateModel comment);
        Task<bool> CommentExists(int id);
        Task<Comment?> DeleteComment(int id);
    }
}
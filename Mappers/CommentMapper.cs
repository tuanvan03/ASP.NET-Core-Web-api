using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedAtTime = commentModel.CreatedAtTime,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateModel(this CommentCreateModel commentCreateModel, int stockId)
        {
            return new Comment
            {
                Title = commentCreateModel.Title,
                Content = commentCreateModel.Content,
                StockId = stockId
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static Comment ToCommentFromRequestDto(this  CreateCommentRequestDto createCommentRequestDto)
        {
            return new Comment {
                Title = createCommentRequestDto.Title,
                Content = createCommentRequestDto.Content,
                CreatedOn = createCommentRequestDto.CreatedOn,
                StockId = createCommentRequestDto.StockId
            };
        }

        public static  CommentDto ToCommentDto(this Comment comment) 
        {
            return new CommentDto{
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
    }
}
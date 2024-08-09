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
        Task<List<Comment>> GETallAsync();
    
        Task<Comment> GETByIdAsync(int id);

        Task<Comment> CREateAsync(Comment commentModel);

        Task<Comment> UPDateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);

        Task<Comment> DELETEAsync(int id) ;
    }
}
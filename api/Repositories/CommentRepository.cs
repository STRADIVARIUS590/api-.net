using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public CommentRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<List<Comment>> GETallAsync(){
            return await _applicationDBContext.Comment.ToListAsync();
        }
    
        public Task<Comment> GETByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> CREateAsync(Comment commentModel)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UPDateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> DELETEAsync(int id) 
        {
             throw new NotImplementedException();
        }
    }
}
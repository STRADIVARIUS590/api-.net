using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ICommentRepository _commentRepository;
        public CommentController(ApplicationDBContext applicationDBContext, ICommentRepository commentRepository)
        {
           _applicationDBContext = applicationDBContext;   
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var comments = await _applicationDBContext.Comment.ToListAsync();
            // return Ok(comments);

            var comments = await _commentRepository.GETallAsync();
            var commentsDto = comments.Select(comment => comment.ToCommentDto());

            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _applicationDBContext.Comment.FindAsync(id);

            if(null == comment) return NotFound();

            return Ok(comment);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto createCommentRequest)
        {
            var commentModel = createCommentRequest.ToCommentFromRequestDto();
    
            await _applicationDBContext.Comment.AddAsync(commentModel);    
            await _applicationDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDto());

         }

        [HttpPut]
        [Route("{id}")]
         public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
         {
            var commentModel = await _applicationDBContext.Comment.FirstOrDefaultAsync(x => x.Id == id);

            if(null == commentModel) return NotFound();

            commentModel.Title = updateCommentRequestDto.Title;
            commentModel.Content = updateCommentRequestDto.Content;
            commentModel.CreatedOn = updateCommentRequestDto.CreatedOn;
            commentModel.StockId = updateCommentRequestDto.StockId;

           await _applicationDBContext.SaveChangesAsync();

            return Ok(commentModel.ToCommentDto());
         }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _applicationDBContext.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

            if(null == commentModel) return NotFound();
        
            _applicationDBContext.Comment.Remove(commentModel);

            await _applicationDBContext.SaveChangesAsync();
        
            return NoContent();
        }
    }    
}
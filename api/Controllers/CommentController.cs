using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.DTOs.Comment;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public CommentController(ApplicationDBContext applicationDBContext)
        {
           _applicationDBContext = applicationDBContext;   
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var comments = _applicationDBContext.Comment.ToList();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var comment = _applicationDBContext.Comment.Find(id);

            if(null == comment) return NotFound();

            return Ok(comment);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentRequestDto createCommentRequest)
        {
            var commentModel = createCommentRequest.ToCommentFromRequestDto();
    
            _applicationDBContext.Comment.Add(commentModel);    

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDto());

         }

        [HttpPut]
        [Route("{id}")]
         public IActionResult Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
         {
            var commentModel = _applicationDBContext.Comment.FirstOrDefault(x => x.Id == id);

            if(null == commentModel) return NotFound();

            commentModel.Title = updateCommentRequestDto.Title;
            commentModel.Content = updateCommentRequestDto.Content;
            commentModel.CreatedOn = updateCommentRequestDto.CreatedOn;
            commentModel.StockId = updateCommentRequestDto.StockId;

            _applicationDBContext.SaveChanges();

            return Ok(commentModel.ToCommentDto());
         }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var commentModel = _applicationDBContext.Comment.FirstOrDefault(comment => comment.Id == id);

            if(null == commentModel) return NotFound();
        
            _applicationDBContext.Comment.Remove(commentModel);

            _applicationDBContext.SaveChanges();
        
            return NoContent();
        }
    }    
}
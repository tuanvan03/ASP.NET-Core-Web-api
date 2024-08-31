using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            var commentDTO = comments.Select(c => c.ToCommentDTO());

            return Ok(commentDTO);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetCommentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CommentCreateModel commentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _stockRepository.StockExists(stockId))
            {
                return NotFound();
            }

            var commentModel = commentCreateModel.ToCommentFromCreateModel(stockId);
            await _commentRepository.CreateComment(commentModel);

            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.ToCommentDTO());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentUpdateModel commentUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentUpdate = await _commentRepository.UpdateComment(id, commentUpdateModel);
            if (commentUpdate == null)
            {
                return NotFound();
            }

            return Ok(commentUpdate.ToCommentDTO());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = await _commentRepository.DeleteComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
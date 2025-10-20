using BookStore.Application.Features.Authors.Commands.UpdateAuthor;
using BookStore.Application.Features.Authors.Queries.GetAllAuthors;
using BookStore.Application.Features.Authors.Commands.CreateAuthor;
using BookStore.Application.Features.Authors.Commands.DeleteAuthor;
using BookStore.Application.Features.Authors.Queries.GetAuthorsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")] // URL sẽ là /api/authors
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        // "Tiêm" MediatR vào Controller
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var query = new GetAllAuthorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var query = new GetAuthorsByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthors command)
        {
            var result = await _mediator.Send(command);
            // Trả về response theo chuẩn RESTful
            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, result);
        }

        // PUT: api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command)
        {
            // Đảm bảo id trong URL khớp với id trong body
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            try
            {
                await _mediator.Send(command);
                return NoContent(); // 204 No Content - Thành công
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
        }

        // DELETE: api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            try
            {
                await _mediator.Send(command);
                return NoContent(); // 204 No Content - Thành công
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
        }
    }
}

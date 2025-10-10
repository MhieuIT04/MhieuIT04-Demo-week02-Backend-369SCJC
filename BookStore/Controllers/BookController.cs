using BookStore.Application.Features.Books.Commands.UpdateBook;
using BookStore.Application.Features.Books.Queries.GetAllBooks;
using BookStore.Application.Features.Books.Commands.CreateBook;
using BookStore.Application.Features.Books.Commands.DeleteBook;
using BookStore.Application.Features.Books.Queries.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")] // URL sẽ là /api/Books
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        // "Tiêm" MediatR vào Controller
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var query = new GetAllBooksQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var query = new GetBookByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            // Trả về response theo chuẩn RESTful
            return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
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

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var command = new DeleteBookCommand { Id = id };
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

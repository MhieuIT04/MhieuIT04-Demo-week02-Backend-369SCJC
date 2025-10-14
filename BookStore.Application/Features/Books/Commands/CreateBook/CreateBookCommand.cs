using MediatR;
using Microsoft.EntityFrameworkCore; // Cần cho AnyAsync
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using System.Collections.Generic; // Cần cho KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Books.Commands.CreateBook
{
    // 1. SỬA LẠI COMMAND: Thêm đầy đủ các trường cần thiết
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public string? Description { get; set; } // Description có thể null
        public int AuthorId { get; set; }
    }

    // 2. SỬA LẠI HANDLER: Xử lý đầy đủ logic
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IApplicationDbContext _context;

        public CreateBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // Logic nghiệp vụ quan trọng: Kiểm tra xem AuthorId có tồn tại không
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == request.AuthorId, cancellationToken);
            if (!authorExists)
            {
                // Ném ra exception để Controller có thể bắt và trả về lỗi 404 hoặc 400
                throw new KeyNotFoundException($"Author with ID {request.AuthorId} not found.");
            }

            var book = new Book
            {
                Title = request.Title,
                PublicationYear = request.PublicationYear,
                Description = request.Description,
                AuthorId = request.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }
    }
}
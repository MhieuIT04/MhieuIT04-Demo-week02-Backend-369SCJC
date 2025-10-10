using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommand: IRequest<Book>
    {
        public string Title { get; set; }
    }
    // Dinh nghia Handler cho CreateBookCommand
    public class CreateBookHandler: IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IApplicationDbContext _context;

        public CreateBookHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
            return book;
        }
    }

}

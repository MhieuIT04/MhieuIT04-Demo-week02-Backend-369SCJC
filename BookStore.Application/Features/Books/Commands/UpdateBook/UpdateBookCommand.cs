using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommand: IRequest<Book?>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    // Dinh nghia Handler cho UpdateBookCommand
    public class UpdateBookHandler: IRequestHandler<UpdateBookCommand , Book>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBookHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);
            if (book == null)
            {
                return null; // Hoac throw new NotFoundException("Book not found");
            }
            book.Title = request.Title;
            await _context.SaveChangesAsync(cancellationToken);
                        return book;

        }
    }
}

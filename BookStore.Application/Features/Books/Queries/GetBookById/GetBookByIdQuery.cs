using Microsoft.EntityFrameworkCore;
using MediatR; 
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Books.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<Book?>
    {
        public int Id { get; set; }
    }

    // Dinh nghia Handler cho GetAuthorsByIdQuery
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
    {
        private readonly IApplicationDbContext _context;

        public GetBookByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books
                               .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        }
    }

}

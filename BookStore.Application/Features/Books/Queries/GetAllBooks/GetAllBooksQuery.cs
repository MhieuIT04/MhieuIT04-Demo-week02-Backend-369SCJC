using Microsoft.EntityFrameworkCore;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;    
using MediatR;

namespace BookStore.Application.Features.Books.Queries.GetAllBooks
{
    public class GetAllBooksQuery: IRequest<IEnumerable<Book>>
    {
    }
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery , IEnumerable<Book>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllBooksQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books.Include(b => b.Author).ToListAsync(cancellationToken);
        }
    }
}

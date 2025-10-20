using Microsoft.EntityFrameworkCore;
using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Authors.Queries.GetAuthorsById
{
    public class GetAuthorsByIdQuery : IRequest<Author?>
    {
        public int Id { get; set; }
    }

    // Dinh nghia Handler cho GetAuthorsByIdQuery
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorsByIdQuery, Author?>
    { 
        private readonly IApplicationDbContext _context;

        public GetAuthorsByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author?> Handle(GetAuthorsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors
                               .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        }
    }

}

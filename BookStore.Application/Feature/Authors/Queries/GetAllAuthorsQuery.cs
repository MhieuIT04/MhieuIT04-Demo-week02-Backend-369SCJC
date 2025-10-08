using MediatR;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Feature.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<Author>>
    {
    }

    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllAuthorsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors.ToListAsync(cancellationToken);
        }
    }
}

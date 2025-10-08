using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Feature.Authors.Commands
{
    public class DeleteAuthors : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public  class DeleteAuthorsHandler : IRequestHandler<DeleteAuthors, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAuthorsHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteAuthors request, CancellationToken cancellationToken)
        {
            var entity = await _context.Authors.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
    
}

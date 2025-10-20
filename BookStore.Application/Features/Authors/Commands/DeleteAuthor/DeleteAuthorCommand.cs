using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    // Dinh nghia Handler cho DeleteAuthorCommand
    public class DeleteAuthorsHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAuthorsHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
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

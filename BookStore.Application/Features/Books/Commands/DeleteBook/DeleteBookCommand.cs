using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Application.Features.Authors.Commands.DeleteAuthor;

namespace BookStore.Application.Features.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    // Handler for DeleteAuthorCommand
    public class DeleteBookHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteBookHandler(IApplicationDbContext context)
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

using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Author?>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    // Dinh nghia Handler cho UpdateAuthors
    public class UpdateAuthorsHandler : IRequestHandler<UpdateAuthorCommand, Author?>
    {
        private readonly IApplicationDbContext _context;
        public UpdateAuthorsHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Author?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(new object[] { request.Id }, cancellationToken);
            if (author == null)
            {
                return null; // Hoac throw new NotFoundException("Author not found");
            }
            author.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return author;
        }
    }

}

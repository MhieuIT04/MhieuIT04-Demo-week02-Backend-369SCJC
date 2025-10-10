using MediatR;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Authors.Commands.CreateAuthor
    {
        public class CreateAuthors : IRequest<Author>
        {
            public string Name { get; set; }

        }

        // Dinh nghia Handler cho CreateAuthors
        public class CreateAuthorHandler : IRequestHandler<CreateAuthors, Author>
        {
            private readonly IApplicationDbContext _context;

            public CreateAuthorHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Author> Handle(CreateAuthors request, CancellationToken cancellationToken)
            { 
                var author = new Author
                {
                    Name = request.Name
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync(cancellationToken);
                return author;
            }
        }
    
    }

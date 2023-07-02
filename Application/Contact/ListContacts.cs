using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class ListContacts
    {
        public record Query() : IRequest<Result<List<Contact>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Contact>>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<List<Contact>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = await context.Contacts.ToListAsync();

                return Result<List<Contact>>.Success(query);
            }
        }
    }
}

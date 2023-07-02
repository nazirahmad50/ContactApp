using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class ContactById
    {
        public record Query(int Id) : IRequest<Result<Contact>>;

        public class Handler : IRequestHandler<Query, Result<Contact>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<Contact>> Handle(Query request, CancellationToken cancellationToken)
            {
                var contact = await context.Contacts.SingleOrDefaultAsync(c => c.Id == request.Id);

                return Result<Contact>.Success(contact);
            }
        }
    }
}

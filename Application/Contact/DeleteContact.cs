using Application.Core;
using MediatR;
using Persistence;

namespace Application
{
    public class DeleteContact
    {
        public record Command(int Id) : IRequest<Result<Unit>> { }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await context.Contacts.FindAsync(request.Id);
                context.Contacts.Remove(product);

                var result = await context.SaveChangesAsync();
                if (result <= 0) return Result<Unit>.Failure("Problem deleting Contact");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

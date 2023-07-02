using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application
{
    public class UpdateContact
    {
        public record Command(int Id, ContactUpdateCreateDto ContactUpdateCreateDto) : IRequest<Result<Contact>> { }

        public class Handler : IRequestHandler<Command, Result<Contact>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<Contact>> Handle(Command request, CancellationToken cancellationToken)
            {
                var contact = await context.Contacts.FindAsync(request.Id);

                mapper.Map(request.ContactUpdateCreateDto, contact);

                context.Update(contact);

                var result = await context.SaveChangesAsync();
                if (result <= 0) return Result<Contact>.Failure("Problem creating contact");

                return Result<Contact>.Success(mapper.Map<Contact>(contact));
            }
        }
    }
}

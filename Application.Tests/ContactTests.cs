using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;

namespace Application.Tests
{
    public class ContactTests
    {
        private IMapper mapper;
        private readonly List<Contact> contacts;

        public ContactTests()
        {
            var myProfile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Bob Smith", PhoneNumber = 1232323 },
                new Contact { Id = 2, Name = "Jake Smith", PhoneNumber = 2323232 }
            };
        }

        private async Task<DataContext> SeedDatabase(List<Contact> contacts)
        {
            var context = new DataContext(CreateNewInMemoryDatabase());
            await context.Database.EnsureCreatedAsync();
            await context.Contacts.AddRangeAsync(contacts);
            await context.SaveChangesAsync();
            return context;
        }

        private DbContextOptions<DataContext> CreateNewInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }


        [Fact]
        public async Task Returns_List_Of_Contacts()
        {
            // Arrange

            using (var context = await SeedDatabase(contacts))
            {
                var query = new ListContacts.Query();
                var handler = new ListContacts.Handler(context);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(contacts, result.Value);
            }
        }

        [Fact]
        public async Task Handle_Create()
        {
            // Arrange

            var contact = new ContactUpdateCreateDto { Name = "Jake Smith", PhoneNumber = 2323232 };
            var returnContact = new Contact { Id = contacts.Count + 1, Name = "Jake Smith", PhoneNumber = 2323232 };

            using (var context = await SeedDatabase(contacts))
            {
                var command = new CreateContact.Command(contact);
                var handler = new CreateContact.Handler(context, mapper);

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equivalent(returnContact, result.Value);
            }
        }
    }
}
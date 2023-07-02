using Domain;

namespace Persistence
{
    public static class SeedData
    {
        public static async Task Initialize(DataContext context)
        {
            if (!context.Contacts.Any())
            {

                var contact = new List<Contact>()
                {
                    new Contact()
                    {
                        Name = "Bob",
                        PhoneNumber = 123456789
                    },

                    new Contact()
                    {
                        Name = "John",
                        PhoneNumber = 987654321
                    },

                    new Contact()
                    {
                        Name = "Dave",
                        PhoneNumber = 232345455
                    },
                };

                foreach (var product in contact)
                {
                    await context.Contacts.AddAsync(product);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

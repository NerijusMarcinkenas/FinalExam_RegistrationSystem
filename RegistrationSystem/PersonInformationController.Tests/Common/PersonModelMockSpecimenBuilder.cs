using AutoFixture.Kernel;
using RegistrationSystem.Core.Models;

namespace RegistrationSystemUnitTests.Common
{
    public class PersonModelMockSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Person))
            {
                return new Person
                {
                    Address = new Address
                    {
                        Street = "Test",
                        BuildingNumber = "22",
                        City = "Test city",
                        FlatNumber = "11",
                        Id = "1",                        
                    },
                    Email = "Test email",
                    FirstName = "Test name",
                    LastName = "Test lastname",
                    PersonalNumber = "32315464",
                    PhoneNumber = "545-550-55",
                    Image = new PersonImage()
                    {
                        Id = "1",
                        ContentType = ".jpg",
                        ImageBytes = new byte[10],
                        Name = "Test",
                        PersonId = "1",
                        Person = null,
                    },
                    User = null,
                    Id = "1",
                    UserId = "1"
                };
            }
            return new NoSpecimen();
        }
    }
}

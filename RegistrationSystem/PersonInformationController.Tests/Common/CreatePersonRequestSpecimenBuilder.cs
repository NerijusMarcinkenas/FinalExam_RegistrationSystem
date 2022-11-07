using AutoFixture.Kernel;
using RegistrationSystem.API.Dtos.Requests;

namespace RegistrationSystemUnitTests.Common
{
    public class CreatePersonRequestSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(CreatePersonRequest))
            {
                return new CreatePersonRequest()
                {
                    CreateAddressRequest = new CreateAddressRequest()
                    {
                        BuildingNumber = "1",
                        City = "Test",
                        FlatNumber = "5",
                        Street = "Test street"
                    },                    
                    CreateImageRequest = null,// new CreateImageRequest(),
                    //{
                    //    PersonImage = s
                    //}
                    Email = "Test email",
                    FirstName = "Test name",
                    LastName = "Test lastname",
                    PersonalNumber = "32315464",
                    PhoneNumber = "545-550-55",
                    
                };
            }
            return new NoSpecimen();
        }
    }
}

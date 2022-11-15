using AutoFixture.Kernel;
using Microsoft.AspNetCore.Http;
using RegistrationSystem.API.Dtos.Requests;

namespace Tests.Common
{
    public class CreatePersonRequestSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(CreatePersonRequest))
            {
                return new CreatePersonRequest()
                {
                    BuildingNumber = "1",
                    City = "Test",
                    FlatNumber = "5",
                    Street = "Test street",
                    PersonImage = CreateFormFile(),
                    Email = "Test email",
                    FirstName = "Test name",
                    LastName = "Test lastname",
                    PersonalNumber = "32315464",
                    PhoneNumber = "545-550-55",
                };
            }
            return new NoSpecimen();
        }

        private static IFormFile CreateFormFile()
        {
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            stream.WriteByte(0);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
            return file;
        }
    }
}

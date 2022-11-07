using AutoFixture.Kernel;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.Models;

namespace RegistrationSystemUnitTests.Common
{
    public class UserSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(User))
            {
                return new User
                {
                    Id = "1",
                    Username = "Test username",
                    Role = Roles.User
                };
            }
            return new NoSpecimen();
        }
    }
}

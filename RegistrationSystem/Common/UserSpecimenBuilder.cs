using AutoFixture.Kernel;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.Models;

namespace Tests.Common
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
                    Role = Roles.User,
                    PasswordHash = new byte[32],
                    PasswordSalt = new byte[32]
                };
            }
            return new NoSpecimen();
        }
    }
}

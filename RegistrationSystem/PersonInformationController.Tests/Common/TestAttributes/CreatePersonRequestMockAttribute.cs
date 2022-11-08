using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;

namespace RegistrationSystemUnitTests.Common.TestAttributes
{
    public class CreatePersonRequestMockAttribute : AutoDataAttribute
    {
        public CreatePersonRequestMockAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                

                fixture.Customizations.Add(new CreatePersonRequestSpecimenBuilder());
                return fixture;
            })
        {
        }
    }

}

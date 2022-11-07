using AutoFixture;
using AutoFixture.Xunit2;

namespace RegistrationSystemUnitTests.Common.TestAttributes
{
    public class AdminUserMockAttribute : AutoDataAttribute
    {
        public AdminUserMockAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new AdminUserSpecimenBuilder());
            return fixture;
        })
        {
        }
    }

}

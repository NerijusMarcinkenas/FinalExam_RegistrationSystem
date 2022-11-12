using AutoFixture;
using AutoFixture.Xunit2;

namespace Tests.Common.TestAttributes
{
    public class UserMockAttribute : AutoDataAttribute
    {
        public UserMockAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserSpecimenBuilder());
            return fixture;
        })
        {

        }
    }
}

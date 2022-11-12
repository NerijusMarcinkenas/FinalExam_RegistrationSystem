using AutoFixture;
using AutoFixture.Xunit2;

namespace Tests.Common.TestAttributes
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

﻿using AutoFixture;
using AutoFixture.Xunit2;

namespace RegistrationSystemUnitTests.Common.TestAttributes
{
    public class PersonModelMockAttribute : AutoDataAttribute
    {
        public PersonModelMockAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new PersonModelMockSpecimenBuilder());
            return fixture;
        })
        {
        }
    }
}

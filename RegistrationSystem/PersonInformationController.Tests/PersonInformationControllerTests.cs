using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.API.Controllers;
using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystemUnitTests.Common;
using RegistrationSystemUnitTests.Common.TestAttributes;
using System.Security.Claims;

namespace PersonInformationControllerTests
{
    public class PersonInformationControllerTests
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly PersonInformationController _sut;

        private readonly IFixture _autofixture;

        public PersonInformationControllerTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _sut = new PersonInformationController(_personServiceMock.Object);
            _autofixture = new Fixture();
            _autofixture.Customizations.Add(new PersonModelMockSpecimenBuilder());

        }

        [Theory, CreatePersonRequestMock, PersonModelMock]
        public async void CreatePerson_WhenUserIsNotAdminAndIdIsNotValid_ReturnsBadRequest(
           
            )
        {
            CreatePersonRequest createPerson = _autofixture.Build<CreatePersonRequest>().With(c => c.CreateImageRequest, new CreateImageRequest()).Create();

            Person person = _autofixture.Create<Person>();

            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("UserId", "1"),
                    new Claim(ClaimTypes.Role, "User")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
           
            var result = await _sut.CreatePerson(createPerson,"2");
            var badRequestResult = result as BadRequestObjectResult;
            
            Assert.Equal(400, badRequestResult.StatusCode);
            _personServiceMock.Verify(a => a.AddPersonAsync(person), Times.Never);

        }
    }
}
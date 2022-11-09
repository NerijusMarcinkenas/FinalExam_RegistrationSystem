using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.API.Common;
using RegistrationSystem.API.Controllers;
using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Extensions;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystem.Core.Services;
using RegistrationSystemUnitTests.Common;
using RegistrationSystemUnitTests.Common.TestAttributes;
using System.Security.Claims;

namespace PersonInformationControllerTests
{
    public class PersonInformationControllerTests
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly PersonInformationController _sut;
        private readonly IFixture _autofixture;

        public PersonInformationControllerTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _imageServiceMock = new Mock<IImageService>();
            _sut = new PersonInformationController(_personServiceMock.Object, _imageServiceMock.Object);
            _autofixture = new Fixture();
            _autofixture.Customizations.Add(new PersonModelMockSpecimenBuilder());
            
        }

        [Theory, CreatePersonRequestMock]
        public async void CreatePerson_WhenUserIsNotAdminAndIdIsNotValid_ReturnsBadRequest(
            CreatePersonRequest createPersonRequest)
        {
            Person person = _autofixture.Create<Person>();

            SeedUserMockRole();

            var result = await _sut.CreatePerson(createPersonRequest, "1");
            var badRequestResult = result as BadRequestObjectResult;
            
            Assert.Equal(400, badRequestResult.StatusCode);
            _personServiceMock.Verify(a => a.AddPersonAsync(person), Times.Never);

        }

        [Theory, CreatePersonRequestMock]
        public async void CreatePerson_WhenUserIsAdmin_ReturnsOk(
          CreatePersonRequest createPersonRequest)
        {
            var person = _autofixture.Create<Person>();
            SeedAdminMockRole();

            var resultMock = new Result<Person>
            {
                IsSuccess = true,
                Message = "",
                ResultObject = person
            };

            _imageServiceMock
                .Setup(x => x.CreateImage(createPersonRequest.CreateImageRequest.PersonImage))
                .Returns(new PersonImage());

            _personServiceMock
                .Setup(x => x.AddPersonAsync(It.Is<Person>(x => x.PersonalNumber == person.PersonalNumber)))
                .ReturnsAsync(resultMock);

            var result = await _sut.CreatePerson(createPersonRequest, "2");
            var okResult = result as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
            _personServiceMock.Verify(a => 
            a.AddPersonAsync(It.Is<Person>(x => x.PersonalNumber == person.PersonalNumber)),
            Times.Once);
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdIsNotValidAndNotAdmin_ReturnsBadRequest()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Never);  
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdIsValidAndPersonIsNotSet_ReturnsBadRequest()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(default(Person));

            var result = await _sut.UpdateFirstName("2", "Test");

            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Never);
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdIsValidAndNotAdmin_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdNotValidAndUserAdmin_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedAdminMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateLastName_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);
            
            var result = await _sut.UpdateLastName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonalNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdatePersonalNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdatePhoneNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdatePhoneNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateEmail_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateEmail("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Theory,CreatePersonRequestMock]
        public async Task UpadateImage_WhenUserIdIsValid_ReturnsOk(CreatePersonRequest createPersonRequest)
        {
            var person = _autofixture.Create<Person>();
            SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);
            _imageServiceMock.Setup(x => x.CreateImage(createPersonRequest.CreateImageRequest.PersonImage)).Returns(new PersonImage());

            var result = await _sut.UpadateImage("2", createPersonRequest.CreateImageRequest);

            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }



        private void SeedUserMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("UserId", "2"),
                    new Claim(ClaimTypes.Role, "User")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }

        private void SeedAdminMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("UserId", "2"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }
    }    
}
using System.Security.Claims;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.API.Controllers;
using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Extensions;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystem.Core.Services;
using RegistrationSystemUnitTests.Common;
using RegistrationSystemUnitTests.Common.TestAttributes;

namespace RegistrationSystemUnitTests
{
    public class PersonInformationControllerTests
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly PersonInformationController _sut;
        private readonly IFixture _fixture;
        private readonly Seeder _seeder;

        public PersonInformationControllerTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _imageServiceMock = new Mock<IImageService>();
            _sut = new PersonInformationController(_personServiceMock.Object, _imageServiceMock.Object);
            _fixture = new Fixture();
            _fixture.Customizations.Add(new PersonModelMockSpecimenBuilder());
            _seeder = new Seeder(_sut);
        }

        [Theory, CreatePersonRequestMock]
        public async void CreatePerson_WhenUserIsNotAdminAndIdIsNotValid_ReturnsBadRequest(
            CreatePersonRequest createPersonRequest)
        {
            Person person = _fixture.Create<Person>();

            _seeder.SeedUserMockRole();

            var result = await _sut.CreatePerson(createPersonRequest, "1");
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.StatusCode.Should().Be(400);
            _personServiceMock.Verify(a => a.AddPersonAsync(person), Times.Never);

        }

        [Theory, CreatePersonRequestMock]
        public async void CreatePerson_WhenUserIsAdmin_ReturnsOk(
          CreatePersonRequest createPersonRequest)
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedAdminMockRole();

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
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Never);  
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdIsValidAndPersonIsNotSet_ReturnsBadRequest()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(default(Person));

            var result = await _sut.UpdateFirstName("2", "Test");

            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Never);
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdIsValidAndNotAdmin_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateFirstName_WhenUserIdNotValidAndUserAdmin_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedAdminMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateFirstName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateLastName_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);
            
            var result = await _sut.UpdateLastName("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonalNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdatePersonalNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdatePhoneNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();
            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdatePhoneNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateEmail_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateEmail("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Theory,CreatePersonRequestMock]
        public async Task UpdateImage_WhenUserIdIsValid_ReturnsOk(CreatePersonRequest createPersonRequest)
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);
            _imageServiceMock.Setup(x => x.CreateImage(createPersonRequest.CreateImageRequest.PersonImage)).Returns(new PersonImage());

            var result = await _sut.UpdateImage("2", createPersonRequest.CreateImageRequest);

            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);
            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public async Task UpdateAddressCity_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateAddressCity("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }
        
        [Fact]
        public async Task UpdateAddressStreet_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateAddressStreet("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }
        
        [Fact]
        public async Task UpdateAddressBuildingNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateAddressBuildingNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }
        
        [Fact]
        public async Task UpdateAddressFlatNumber_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.UpdateAddressFlatNumber("2", "Test");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

            _personServiceMock.Verify(x => x.UpdatePerson(person), Times.Once);
        }
        
        [Fact]
        public async Task GetPersonByUserId_WhenUserIdIsValid_ReturnsOk()
        {
            var person = _fixture.Create<Person>();
            _seeder.SeedUserMockRole();

            _personServiceMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(person);

            var result = await _sut.GetPersonByUserId("2");
            var resultAsOk = result as OkObjectResult;

            Assert.Equal(200, resultAsOk.StatusCode);

        }

    }    
}
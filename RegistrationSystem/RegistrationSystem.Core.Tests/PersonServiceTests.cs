using AutoFixture;
using FluentAssertions;
using Moq;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystem.Core.Services;
using Tests.Common;

namespace RegistrationSystem.Core.Tests;

public class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly IPersonService _sut;
    private readonly Fixture _fixture;

    public PersonServiceTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _sut = new PersonService(_personRepositoryMock.Object);
        _fixture = new Fixture();
        _fixture.Customizations.Add(new PersonModelMockSpecimenBuilder());
    }

    [Fact]
    public async void GetPersonWithIncludesAsync_WhenNotFount_Success()
    {
        _personRepositoryMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(default(Person));

        var result = await _sut.GetPersonWithIncludesAsync("2");

        Assert.Null(result);
        _personRepositoryMock.Verify(x => x.GetPersonWithIncludesAsync("2"), Times.Once);
    }

    [Fact]
    public async void GetPersonWithIncludesAsync_WhenIsFound_Success()
    {
        _personRepositoryMock.Setup(x => x.GetPersonWithIncludesAsync("2")).ReturnsAsync(new Person());

        var result = await _sut.GetPersonWithIncludesAsync("2");

        Assert.NotNull(result);
        _personRepositoryMock.Verify(x => x.GetPersonWithIncludesAsync("2"), Times.Once);
    }

    [Fact]
    public async Task AddPersonAsync_WhenUserNotFound_ReturnsResultTrue()
    {
        var personMock = _fixture.Create<Person>();

        _personRepositoryMock.Setup(x => x.GetByIdAsync(personMock.UserId)).ReturnsAsync(default(Person));
        _personRepositoryMock.Setup(x => x.AddAsync(personMock)).ReturnsAsync(new Person());

        var result = await _sut.AddPersonAsync(personMock);

        result.IsSuccess.Should().Be(true);
        result.Message.Should().Be("Person added successfully");
        result.ResultObject.Should().NotBeNull();

        _personRepositoryMock.Verify(x => x.GetByIdAsync(personMock.UserId), Times.Once);
        _personRepositoryMock.Verify(x => x.AddAsync(personMock), Times.Once);
    }

    [Fact]
    public async Task AddPersonAsync_WhenPersonAlreadyExists_ReturnsResultFalse()
    {
        var personMock = _fixture.Create<Person>();

        _personRepositoryMock.Setup(x => x.GetByIdAsync(personMock.UserId)).ReturnsAsync(personMock);

        var result = await _sut.AddPersonAsync(personMock);

        result.IsSuccess.Should().Be(false);
        result.Message.Should().Be("Person information could be added only one time");

        _personRepositoryMock.Verify(x => x.GetByIdAsync(personMock.UserId), Times.Once);
        _personRepositoryMock.Verify(x => x.AddAsync(null!), Times.Never);
    }

    [Fact]
    public async Task UpdatePerson_WhenPersonIsValid_Success()
    {
        var personMock = _fixture.Create<Person>();
        _personRepositoryMock.Setup(x => x.UpdateAsync(personMock)).ReturnsAsync(personMock);

        var result = await _sut.UpdatePerson(personMock);

        result.Should().NotBeNull();
        _personRepositoryMock.Verify(x => x.UpdateAsync(personMock), Times.Once);
    }

}
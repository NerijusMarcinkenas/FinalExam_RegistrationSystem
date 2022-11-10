using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.API.Controllers;
using RegistrationSystem.API.Dtos.AuthorizationDtos;
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystemUnitTests.Common;

namespace RegistrationSystemUnitTests;

public class AuthorizationControllerTests
{
    private readonly Mock<IUserAccountService> _accountServiceMock;
    private readonly AuthorizationController _sut;
    private readonly Fixture _fixture;
    private readonly Seeder _seeder;
    public AuthorizationControllerTests()
    {
        _accountServiceMock = new Mock<IUserAccountService>();
        _sut = new AuthorizationController(_accountServiceMock.Object);
        _fixture = new Fixture();
        _seeder = new Seeder(_sut);
    }
    
    [Fact]
    public async Task RegisterUser_WhenResultIsNotSuccess_ReturnsBadRequest()
    {
        var userRequestMock = _fixture.Build<RegisterUserRequest>().Create();
        _accountServiceMock
            .Setup(x =>
                x.CreateUserAccountAsync(userRequestMock.Username, userRequestMock.Password))
            .ReturnsAsync(new AuthorizationResult<User>());

        var result = await _sut.RegisterUser(userRequestMock);
        var resultAsBadRequest = result as BadRequestObjectResult;

        resultAsBadRequest.StatusCode.Should().Be(400);
    }
    
    [Fact]
    public async Task RegisterUser_WhenResultIsSuccess_ReturnsOk()
    {
        var userRequestMock = _fixture.Build<RegisterUserRequest>().Create();
        
        _accountServiceMock
            .Setup(x =>
                x.CreateUserAccountAsync(userRequestMock.Username, userRequestMock.Password))
            .ReturnsAsync(new AuthorizationResult<User>{IsSuccess = true});

        var result = await _sut.RegisterUser(userRequestMock);
        var resultAsOkResult = result as OkObjectResult;

        resultAsOkResult.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task LoginUser_WhenResultIsNotSuccess_ReturnsBadRequest()
    {
        var userRequestMock = _fixture.Build<LoginUserRequest>().Create();
        
        _accountServiceMock
            .Setup(x => 
                x.LoginUserAsync(userRequestMock.Username, userRequestMock.Password))
            .ReturnsAsync(new AuthorizationResult<User>());

        var result = await _sut.LoginUser(userRequestMock);
        var resultAsBadRequest = result as BadRequestObjectResult;

        resultAsBadRequest.StatusCode.Should().Be(400);
    }
    
    [Fact]
    public async Task LoginUser_WhenResultIsSuccess_ReturnsOk()
    {
        var userRequestMock = _fixture.Build<LoginUserRequest>().Create();
        
        _accountServiceMock
            .Setup(x =>
                x.LoginUserAsync(userRequestMock.Username, userRequestMock.Password))
            .ReturnsAsync(new AuthorizationResult<User>{IsSuccess = true});

        var result = await _sut.LoginUser(userRequestMock);
        var resultAsOkResult = result as OkObjectResult;

        resultAsOkResult.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task RemoveUserAccount_WhenUserIsAdminAndIdIsAdminId_ReturnsBadRequest()
    {
        _seeder.SeedAdminMockRole();
        
        var result = await _sut.RemoveUserAccount("2");
        var badRequestResult = result as BadRequestObjectResult;

        badRequestResult.StatusCode.Should().Be(400);
        _accountServiceMock.Verify(x => x.RemoveUserAccountAsync("2"), Times.Never);
    }
    
    [Fact]
    public async Task RemoveUserAccount_WhenUserIsAdminAndIdIsNotAdminId_ReturnsOk()
    {
        _seeder.SeedAdminMockRole();
        _accountServiceMock
            .Setup(x => x.RemoveUserAccountAsync("1"))
            .ReturnsAsync(new Result<User> { IsSuccess = true });
        
        var result = await _sut.RemoveUserAccount("1");
        var resultAsOkResult = result as OkObjectResult;

        resultAsOkResult.StatusCode.Should().Be(200);
        _accountServiceMock.Verify(x => x.RemoveUserAccountAsync("1"), Times.Once);
    }

    [Fact]
    public async Task RemoveUserAccount_WhenResultIsNotSuccess_ReturnsNotFound()
    {
        _seeder.SeedAdminMockRole();
        _accountServiceMock
            .Setup(x => x.RemoveUserAccountAsync("1"))
            .ReturnsAsync(new Result<User>{IsSuccess = false});
        
        var result = await _sut.RemoveUserAccount("1");
        var resultAsOkResult = result as NotFoundObjectResult;

        resultAsOkResult.StatusCode.Should().Be(404);
        _accountServiceMock.Verify(x => x.RemoveUserAccountAsync("1"), Times.Once);
    }
    
    
}
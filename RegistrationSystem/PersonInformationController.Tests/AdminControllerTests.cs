using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.API.Controllers;
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using Tests.Common;

namespace RegistrationSystemUnitTests;

public class AdminControllerTests
{
    private readonly Mock<IUserAccountService> _accountServiceMock;
    private readonly AdminController _sut;
    private readonly Seeder _seeder;
    
    public AdminControllerTests()
    {
        _accountServiceMock = new Mock<IUserAccountService>();
        _sut = new AdminController(_accountServiceMock.Object);
        _seeder = new Seeder(_sut);
    }
    
    [Fact]
    public async Task RemoveUserAccount_WhenUserIsAdminAndIdIsAdminId_ReturnsBadRequest()
    {
        _seeder.SeedAdminMockRole();
        _accountServiceMock.Setup(x => x.RemoveUserAccountAsync("2"))
            .ReturnsAsync(new Result<User>());
        
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
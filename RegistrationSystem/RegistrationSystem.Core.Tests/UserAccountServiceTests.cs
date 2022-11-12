using FluentAssertions;
using Moq;
using RegistrationSystem.Core.IdentityServices;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using Tests.Common.TestAttributes;

namespace RegistrationSystem.Core.Tests
{
    public class UserAccountServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IIdentityJwtTokenService> _jwtServiceMock;
        private readonly UserAccountService _sut;

        public UserAccountServiceTests()
        {
            _jwtServiceMock = new Mock<IIdentityJwtTokenService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new UserAccountService(_userRepositoryMock.Object, _jwtServiceMock.Object);           
        }

        [Theory, UserMock]
        public async Task CreateUserAccountAsync_WhenUsernameIsTaken_ReturnsFalseAndMessage(User user) 
        {
            _userRepositoryMock.Setup(x => x.IsUserExistsAsync("test")).ReturnsAsync(true);

            var result = await _sut.CreateUserAccountAsync("test", "test");

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("already exist");

            _userRepositoryMock.Verify(x => x.AddAsync(user), Times.Never);
        }

        [Theory, UserMock]
        public async Task CreateUserAccountAsync_WhenUsernameIsValid_ReturnsTrueAndMessage(User user)
        {
            _userRepositoryMock.Setup(x => x.IsUserExistsAsync("test")).ReturnsAsync(false);

            var result = await _sut.CreateUserAccountAsync(user.Username, "test");

            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("successfully");

            _userRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(x => x.Username == user.Username)), Times.Once);
        }

        [Theory, UserMock]
        public async Task LoginUserAsync_WhenUserNotFound_ReturnsFalseAndMessage(User user)
        {
            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(user.Username)).ReturnsAsync(default(User));

            var result = await _sut.LoginUserAsync(user.Username, "test");

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Wrong");
        }

        [Theory, UserMock]
        public async Task RemoveUserAccountAsync_WhenUserIsNotFound_ReturnsFalseAndMessage(User user)
        {
            _userRepositoryMock.Setup(x => x.GetUserWithPersonAsync(user.Id)).ReturnsAsync(default(User));

            var result = await _sut.RemoveUserAccountAsync(user.Id);

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("not found");

            _userRepositoryMock.Verify(x => x.RemoveAsync(user), Times.Never);
        }

        [Theory, UserMock]
        public async Task RemoveUserAccountAsync_WhenUserIsValid_ReturnsTrueAndMessage(User user)
        {
            _userRepositoryMock.Setup(x => x.GetUserWithPersonAsync(user.Id)).ReturnsAsync(user);

            var result = await _sut.RemoveUserAccountAsync(user.Id);

            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("successfully");
            _userRepositoryMock.Verify(x => x.RemoveAsync(user), Times.Once);

        }
    }
}

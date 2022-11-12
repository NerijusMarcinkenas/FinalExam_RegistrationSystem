using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationSystem.Core.IdentityServices
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityJwtTokenService _jwtTokenService;

        public UserAccountService(IUserRepository userRepository,
            IIdentityJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthorizationResult<User>> CreateUserAccountAsync(string username, string password)
        {
            var isUserExist = await _userRepository.IsUserExistsAsync(username);

            var result = new AuthorizationResult<User>();
            if (isUserExist)
            {
                result.Message = $"User with username {username} already exist";
                return result;
            }

            var (hash, salt) = CreatePasswordHash(password);
            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = Roles.User,
            };

            await _userRepository.AddAsync(user);

            result.IsSuccess = true;
            result.Message = "User created successfully";
            return result;
        }

        public async Task<AuthorizationResult<User>> LoginUserAsync(string username, string password)
        {
            var result = new AuthorizationResult<User>();

            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                result.Message = "Wrong credentials";
                return result;
            }

            result.IsSuccess = true;
            result.Message = "Login successfully";
            result.Token = _jwtTokenService.GetJwtToken(user);
            return result;
        }

        public async Task<Result<User>> RemoveUserAccountAsync(string userId)
        {
            var userToRemove = await _userRepository.GetUserWithPersonAsync(userId);

            var result = new Result<User>();

            if (userToRemove == null)
            {
                result.Message = $"User with id: {userId} not found";
                return result;
            }

            await _userRepository.RemoveAsync(userToRemove);

            result.Message = "User removed successfully";
            result.IsSuccess = true;

            return result;
        }

        private static (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}

using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationSystem.Core.IdentityServices
{

    internal class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityJwtTokenService _jwtTokenService;

        public UserAccountService(IUserRepository userRepository,
            IIdentityJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IAuthorizationResult<User>> CreateUserAccountAsync(string username, string password)
        {
            var isUserExist = await _userRepository.IsUserExists(username);
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
            result.Message = "User created sucessfully";
            return result;
        }

        public async Task<IAuthorizationResult<User>> LoginUserAsync(string username, string password)
        {
            var result = new AuthorizationResult<User>();
            if (!await _userRepository.IsUserExists(username))
            {
                result.Message = "Wrong username";
                return result;
            }

            var user = await _userRepository.GetUserByUsername(username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                result.Message = "Wrong password";
                return result;
            }

            result.IsSuccess = true;
            result.Message = "Login successfull";
            result.Token = _jwtTokenService.GetJwtToken(user);
            return result;
        }

        public async Task<IResult<User>> RemoveUserAccountAsync(string userId)
        {
            var userToRemove = await _userRepository.GetUserWithPerson(userId);
            if (userToRemove == null)
            {
                return new Result<User>
                {
                    Message = $"User with id: {userId} not found"
                };
            }

            await _userRepository.RemoveAsync(userToRemove);
            return new Result<User>
            {
                Message = "User remove successfully",
                IsSuccess = true
            };
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

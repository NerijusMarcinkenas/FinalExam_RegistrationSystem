using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.API.Dtos.AuthorizationDtos;
using RegistrationSystem.API.Errors;
using RegistrationSystem.API.Extensions;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserAccountService _accountService;

        public AuthorizationController(IUserAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> RegisterUser(RegisterUserRequest userRequest)
        {
            var result = await _accountService.CreateUserAccountAsync(
                username: userRequest.Username,
                password: userRequest.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(new BadRequestMessage(result.Message));
            }

            var response = new AuthorizationResponse<User>(result);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser(LoginUserRequest userRequest)
        {
            var result = await _accountService.LoginUserAsync(
                username: userRequest.Username,
                password: userRequest.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(new BadRequestMessage(result.Message));
            }

            var response = new AuthorizationResponse<User>(result);
            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("RemoveUserAccount")]
        public async Task<ActionResult> RemoveUserAccount(string userId)
        {
            if (User.MatchProvidedId(userId))
            {
                return BadRequest(new BadRequestMessage(AdminCannotRemoveSelf.Message));
            }

            var result = await _accountService.RemoveUserAccountAsync(userId);
            
            if (!result.IsSuccess)
            {
                return NotFound(new NotFoundErrorMessage(result.Message));
            }

            return Ok(result);  
        }
    }
}

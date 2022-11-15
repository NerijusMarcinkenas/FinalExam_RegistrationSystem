using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.API.Errors;
using RegistrationSystem.API.Extensions;
using RegistrationSystem.Core.Interfaces;

namespace RegistrationSystem.API.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserAccountService _accountService;

        public AdminController(IUserAccountService accountService)
        {
            _accountService = accountService;
        }
                
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

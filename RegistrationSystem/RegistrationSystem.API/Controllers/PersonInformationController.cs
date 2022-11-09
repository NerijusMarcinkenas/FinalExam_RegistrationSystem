using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.API.Common;
using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.API.Dtos.Responses;
using RegistrationSystem.API.Errors;
using RegistrationSystem.API.Extensions;
using RegistrationSystem.Core.Extensions;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Controllers
{
    [Authorize(Policy = "User")]
    [Route("[controller]")]
    [ApiController]
    public class PersonInformationController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IImageService _imageService;
        private readonly PersonMapper _personMapper;

        public PersonInformationController(IPersonService personService,
            IImageService imageService)
        {
            _personService = personService;
            _imageService = imageService;
            _personMapper = new PersonMapper(imageService);
        }

        [HttpPost("CreatePerson")]
        public async Task<ActionResult> CreatePerson(
            [FromForm] CreatePersonRequest createPersonRequest, 
            string userId)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var person = _personMapper.MapPersonRequest(createPersonRequest, userId);
            
            var result =  await _personService.AddPersonAsync(person);

            if (!result.IsSuccess || result.ResultObject == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var resultResponse = new ResultResponse<PersonResponse>
            {
                ObjectResult = new PersonResponse(result.ResultObject),
                Message = result.Message
            };

            return Ok(resultResponse);
        }

        [HttpPut("UpdateFirstName")]
        public async Task<ActionResult> UpdateFirstName(
            [Required]string userId,
            [Required]string firstName)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);

            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateFirstNameAsync(personToUpdate, firstName);  
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdateLastName")]
        public async Task<ActionResult> UpdateLastName(
            [Required] string userId,
            [Required] string lastName)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);  
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateLastNameAsync(personToUpdate, lastName);
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdatePersonalNumber")]
        public async Task<ActionResult> UpdatePersonalNumber(
            [Required] string userId, 
            [Required] string personalNumber)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpadatePersonalNumberAsync(personToUpdate, personalNumber);
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdatePhoneNumber")]
        public async Task<ActionResult> UpdatePhoneNumber(
            [Required] string userId, 
            [Required] string phoneNumber)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdatePhoneNumberAsync(personToUpdate, phoneNumber);
            var resultResponse = new PersonResponse(updatedPerson);

            return Ok(resultResponse);
        }

        [HttpPut("UpdateEmail")]
        public async Task<ActionResult> UpdateEmail(
            [Required] string userId, 
            [Required] string email)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateEmailAsync(personToUpdate, email);
            var resultResponse = new PersonResponse(updatedPerson);

            return Ok(resultResponse);
        }

        [HttpPut("UpadateImage")]
        public async Task<ActionResult> UpadateImage(
            [Required] string userId, 
            [Required] CreateImageRequest imageRequest)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);

            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var personImage = _imageService.CreateImage(imageRequest.PersonImage);

            var updatedPerson = await _personService
                .UpadateImageAsync(personToUpdate, personImage);

            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }


        [HttpPut("UpdateAddressCity")]
        public async Task<ActionResult> UpdateAddressCity(
            [Required] string userId, 
            [Required] string addressCity)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateAddressCityAsync(personToUpdate, addressCity);
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdateAddressStreet")]
        public async Task<ActionResult> UpdateAddressStreet(
            [Required] string userId,
            [Required] string addressStreet)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateAddressStreetAsync(personToUpdate, addressStreet);
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdateAddressBuildingNumber")]
        public async Task<ActionResult> UpdateAddressBuildingNumber(
            [Required] string userId, 
            [Required] string buildingNumber)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateAddressBuildingNumberAsync(personToUpdate, buildingNumber);
            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpPut("UpdateAddressFlatNumber")]
        public async Task<ActionResult> UpdateAddressFlatNumber(
            [Required] string userId, 
            [Required] string flatNumber)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));

            var personToUpdate = await _personService.GetPersonWithIncludesAsync(userId);
            if (personToUpdate == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var updatedPerson = await _personService.UpdateAddressFlatNumberAsync(personToUpdate, flatNumber);

            var resultResponse = new PersonResponse(updatedPerson);
            return Ok(resultResponse);
        }

        [HttpGet("GetPersonByUserId")]
        public async Task<ActionResult> GetPersonByUserId(string userId)
        {
            if (!User.IsAdmin() && !User.MatchProvidedId(userId))
                return BadRequest(new BadRequestMessage(LogedInUserIdDontMatchUserId.Message));
            
            var person = await _personService.GetPersonWithIncludesAsync(userId);
            if (person == null)
            {
                return BadRequest(new BadRequestMessage(PersonNotCreatedMessage.Message));
            }

            var personResponse = new PersonResponse(person);
            return Ok(personResponse);
        }
    }
}

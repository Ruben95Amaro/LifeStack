using Application.Commands;
using Application.DTOs;
using Application.Mappers;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController(ISender sender) : ControllerBase
    {
        // HTTP POST endpoint to create a new user.
        // Accepts a UserDTO from the request body.
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = await sender.Send(new AddUserCommand(userDTO));

                var routeValues = new { userId = result.Value.Id };
                return CreatedAtAction(
                    nameof(GetUserByIdAsync),
                    routeValues,
                    routeValues
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string userId)
        {
            var result = await sender.Send(new GetUserByIdQuery(userId));
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UserDTO userDTO)
        {

            var result = await sender.Send(new UpdateUserCommand(userId, userDTO));
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }
}

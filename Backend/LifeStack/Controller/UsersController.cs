using Application.Commands;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserEntities user)
        {
            var result = await sender.Send(new AddUserCommand(user));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var result = await sender.Send(new GetUserByIdQuery(userId));
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UserEntities user)
        {
            var result = await sender.Send(new UpdateUserCommand(userId, user));
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }
}

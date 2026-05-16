using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Login;
using Application.Users.Queries;
using Domain.Users.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Web.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        public readonly ISender _sender;
        public readonly UserManager<UserEntity> _userManager;
        public readonly RoleManager<UserRoles> _roleManager;
        public readonly IConfiguration _configuration;

        public UsersController(ISender sender, UserManager<UserEntity> userManager, RoleManager<UserRoles> roleManager, IConfiguration configuration)
        {
            _sender = sender;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // HTTP POST endpoint to create a new user.
        // Accepts a UserDTO from the request body.
        [HttpPost("")]
        public async Task<IActionResult> RegisteUserAsync([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = await _sender.Send(new RegisteUserCommand(userDTO));

                if (result.IsFailure)
                    return Conflict(result.Error.Description);
                
                
                var routeValues = new { email = result.Value.Email };

                return CreatedAtAction(
                    nameof(GetByEmail),
                    routeValues,
                    routeValues
                );

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _sender.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [Authorize]

        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetUserByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result.Error.Description);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize]

        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {

            var result = await _sender.Send(new UpdateUserCommand(id, userDTO));
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]

        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new DeleteUserCommand(id));
            return Ok(result);
        }

        [HttpGet("email")]

        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _sender.Send(new GetUserByEmailQuery(email));
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO )
        {
            var result = await _sender.Send(new LoginUserCommand(loginDTO));

            if (result.IsFailure)
                return Unauthorized(result.Error.Description);


            return Ok(result.Value);
            
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.DTOs
{
    public class UserDTO
    {
        public required string? FirstName { get; set; } = null!;
        public required string? LastName { get; set; } = null!;

        public required string? UserName { get; set; } = null!;
        public required string? Email { get; set; } = null!;
        public required string? PhoneNumber { get; set; } = null!;

        public required string? Password { get; set; } = null!;

    }
}

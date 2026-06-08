using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.DTOs
{
    public class LoginResponseDTO
    {
        public string? Email {  get; set; }
        public string? AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.Entities
{
    public class UserRoles : IdentityRole<Guid>
    {
        public string Role { get; set; }
        public string UserId { get; set; }

    }
}


using Microsoft.AspNet.Identity.EntityFramework;

using SharedKernel;

namespace Domain.Users
{
    public sealed class User : IdentityUser
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public String? CreateAt {  get; set; }

    }
}

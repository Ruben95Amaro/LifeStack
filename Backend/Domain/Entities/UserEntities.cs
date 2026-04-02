using Microsoft.AspNetCore.Identity;


namespace Domain.Entities
{
    public class UserEntities : IdentityUser
    {


        public string CreatedAt { get; set; } = null!;


        public string? FirstName { get; private set; } = null!;
        public string? LastName { get; private set; } = null!;

        private UserEntities() { }

        public UserEntities(string firstName, string lastName, string userName, string email, string passwordHash)
        {
            SetUserName(userName);
            SetName(firstName, lastName);
            SetEmail(email);
            CreatedAt = DateTime.UtcNow.ToString();
        }

        public void SetName(string? firstName, string? lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && firstName != FirstName )
                FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName) && lastName != LastName)
                LastName = lastName;
        }

        public void SetEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@") && email != Email)
                Email = email;
        }

        public void SetUserName(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName) && userName != UserName)
                UserName = userName;
        }

    }
}

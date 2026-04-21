using Microsoft.AspNetCore.Identity;
using SharedKernel.InfoValidation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Domain.Users.Entities
{

    public class UserEntity : IdentityUser
    {
        public string CreatedAt { get; private set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; private set;   }
        [Required]
        public string FirstName { get; private set; } = null!;
        [Required]
        public string LastName { get; private set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public DateTime? Birthday { get; private set; }

        private UserEntity() { }

        public UserEntity(string firstName, string lastName, string email)
        {
            SetName(firstName, lastName);
            SetEmail(email);
            CreatedAt = DateTime.UtcNow.ToString();
        }

        public Result Update(string firstName, string lastName, string email)
        {
            Result setEmailResult = SetEmail(email);

            if (!setEmailResult.IsSuccess) return setEmailResult;

            Result SetNameResult = SetName(firstName, lastName);


            if (!SetNameResult.IsSuccess) return SetNameResult;

            CreatedAt = DateTime.UtcNow.ToString();
            return Result.Success(new { id = this.Id });
        }

        public Result SetName(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) 
                return Result.Failure(UserErrors.RequiredFirstName);

            if (string.IsNullOrWhiteSpace(lastName)) 
                return Result.Failure(UserErrors.RequiredLastName);


            if (firstName != FirstName) FirstName = firstName;

            if (lastName != LastName) LastName = lastName;

            return Result.Success();
        }

        public Result SetBirthday(DateTime birthday)
        {
            if (birthday > DateTime.UtcNow)
                return Result.Failure(UserErrors.BirthDayNotInTheFuture);

            if (Birthday != Birthday)
                Birthday = birthday;

            return Result.Success();

        }
        public Result SetEmail(string email)
        {
            email = email.Trim();

            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure(UserErrors.RequiredEmail);

            if (!email.Contains("@"))
                return Result.Failure(UserErrors.NotAtInEmail);

            if (email == Email)
                return Result.Success();

            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = email.ToUpper();

            return Result.Success();

        }   

    }
}

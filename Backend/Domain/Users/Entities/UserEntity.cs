using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using SharedKernel.InfoValidation;
using System.ComponentModel;

public class UserEntity : IdentityUser<Guid>
{

    public string CreatedAt { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateTime? Birthday { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public bool IsDeleted { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }

    private UserEntity() { }

    private UserEntity(Guid id, string email, string firstName, string lastName, string passwordHash)
    {
        Id = id;

        SetEmail(email);
        SetName(firstName, lastName);
        SetPassword(passwordHash);

        CreatedAt = DateTime.UtcNow.ToString();
    }

    public static Result<UserEntity> CreateUser(string email, string firstName, string lastName, string passwordHash)
    {
        var user = new UserEntity();

        var emailResult = user.SetEmail(email);
        if (emailResult.IsFailure) return Result.Failure(emailResult.Error);

        var nameResult = user.SetName(firstName, lastName);
        if (nameResult.IsFailure) return Result.Failure(nameResult.Error);

        var passResult = user.SetPassword(passwordHash);
        if (passResult.IsFailure) return Result.Failure(passResult.Error);

        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow.ToString();

        return Result<UserEntity>.Success(user);
    }
    public Result Update(string firstName, string lastName, string email, string passwordHash)
    {
        var emailResult = SetEmail(email);
        if (emailResult.IsFailure) return emailResult;

        var nameResult = SetName(firstName, lastName);
        if (nameResult.IsFailure) return nameResult;

        var passResult = SetPassword(passwordHash);
        if (passResult.IsFailure) return passResult;

        return Result.Success();
    }


    public Result SetName(string? firstName, string? lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure(UserErrors.RequiredFirstName);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure(UserErrors.RequiredLastName);

        FirstName = firstName;
        LastName = lastName;

        return Result.Success();
    }

    public Result SetBirthday(DateTime birthday)
    {
        if (birthday > DateTime.UtcNow)
            return Result.Failure(UserErrors.BirthDayNotInTheFuture);

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

        Email = email;
        UserName = email;
        NormalizedEmail = email.ToUpper();
        NormalizedUserName = email.ToUpper();

        return Result.Success();
    }

    public Result SetPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result.Failure(UserErrors.RequiredEmail);

        if (passwordHash.Length < 6)
            return Result.Failure(UserErrors.RequiredEmail);

        PasswordHash = passwordHash;
        return Result.Success();
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

}
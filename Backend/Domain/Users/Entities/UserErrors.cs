using SharedKernel.InfoValidation;


namespace Domain.Users.Entities
{
    public static class UserErrors
    {
        public static Error NotFound(Guid userId) => Error.NotFound(
            "Users.NotFound",
            $"The user with the Id = '{userId}' was not found");


        public static Error Unauthorized() => Error.Failure(
            "Users.Unauthorized",
            "You are not authorized to perform this action.");

        public static readonly Error NotFoundByEmail = Error.NotFound(
            "Users.NotFoundByEmail",
            "The user with the specified email was not found");

        public static readonly Error NotFoundById = Error.NotFound(
           "Users.NotFoundById",
           "The user with the specified Id was not found");

        public static readonly Error NotValidEmail = Error.NotFound(
        "Users.NotValidEmail",
        "The Email is not valid");

        public static readonly Error NotAtInEmail = Error.NotFound(
        "Users.NotAtInEmail",
        "The Email doesn't contain @");

        public static readonly Error EmailNotUnique = Error.Conflict(
            "Users.EmailNotUnique",
            "The provided email is not unique");

        public static readonly Error RequiredEmail = Error.Conflict(
            "Users.RequiredEmail",
            "Email is required");

        public static readonly Error RequiredFirstName = Error.Conflict(
            "Users.RequiredFirstName",
             "First name is required");

        public static readonly Error RequiredLastName = Error.Conflict(
    "Users.RequiredLastName",
     "LastName name is required");


        public static readonly Error BirthDayNotInTheFuture = Error.Conflict(
    "Users.BirthDayNotInTheFuture",
     "Birthday cannot be in the future");

    }
}

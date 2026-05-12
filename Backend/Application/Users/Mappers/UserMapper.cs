using Application.Users.DTOs;
using Microsoft.AspNetCore.Identity;
using SharedKernel.InfoValidation;


namespace Application.Users.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(UserEntity user) => new UserDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Password = user.PasswordHash
        };

        public static Result<UserEntity> FromDTO(UserDTO userDTO)
        {
            var resultUserCreation = UserEntity.CreateUser(
                userDTO.Email,
                userDTO.FirstName,
                userDTO.LastName,
                userDTO.Password
            );

            if (resultUserCreation.IsFailure)
                return Result.Failure<UserEntity>(resultUserCreation.Error);

            return Result.Success(resultUserCreation.Value);
        }
    }
}

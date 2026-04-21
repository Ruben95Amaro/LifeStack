using Application.Users.DTOs;
using Domain.Users.Entities;

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
            UserName = user.UserName
        };

        public static UserEntity FromDTO(UserDTO userDTO)
        {
            return new UserEntity(userDTO.FirstName, userDTO.LastName, userDTO.Email ); // userDTO.PhoneNumber
        }
    }
}

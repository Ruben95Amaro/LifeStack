using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(UserEntity user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }

        public static UserEntity FromDTO(UserDTO userDTO)
        {
            return new UserEntity(userDTO.FirstName, userDTO.LastName, userDTO.UserName, userDTO.Email, userDTO.PhoneNumber);
        }
    }
}

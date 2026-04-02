using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(UserEntities user)
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

        public static UserEntities FromDTO(UserDTO userDTO)
        {
            return new UserEntities(userDTO.FirstName, userDTO.LastName, userDTO.UserName, userDTO.Email, userDTO.PhoneNumber);
        }
    }
}

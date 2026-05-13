using Application.Users.DTOs;
using Domain.Users;
using Domain.Users.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    LoginResponseDTO Create(UserEntity user);
}

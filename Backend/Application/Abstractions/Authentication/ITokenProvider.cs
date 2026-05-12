using Domain.Users;
using Domain.Users.Entities;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(UserEntity user);
}

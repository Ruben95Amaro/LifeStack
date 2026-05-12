using Application.Abstractions.Authentication;
using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Mappers;
using Domain.Users;
using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.InfoValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.Login
{
    public record LoginUserCommand(LoginDTO LoginDTO)
      : IRequest<Result<string>>;

    public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
: IRequestHandler<LoginUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            {

                UserEntity? user = await userRepository.GetByEmailAsync(request.LoginDTO.Email);

                if (user is null)                    
                    return Result.Failure<string>(UserErrors.NotFoundByEmail);


                bool verified = passwordHasher.Verify(request.LoginDTO.Password, user.PasswordHash);

                if (!verified)
                    return Result.Failure<string>(UserErrors.PasswordInvalid);

                string token = tokenProvider.Create(user);

                return Result.Success( token );
            }
        }
    }
}

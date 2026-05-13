using Application.Abstractions.Authentication;
using Application.Users.DTOs;
using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;

using SharedKernel.InfoValidation;


namespace Application.Users.Login
{
    public record LoginUserCommand(LoginDTO LoginDTO)
      : IRequest<Result<LoginResponseDTO>>;

    public class LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider)
    : IRequestHandler<LoginUserCommand, Result<LoginResponseDTO>>
    {
        public async Task<Result<LoginResponseDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            {

                UserEntity? user = await userRepository.GetByEmailAsync(request.LoginDTO.Email);

                if (user is null)                    
                    return Result.Failure(UserErrors.NotFoundByEmail);


                bool verified = passwordHasher.Verify(request.LoginDTO.Password, user.PasswordHash);

                if (!verified)
                    return Result.Failure(UserErrors.PasswordInvalid);

                var token = tokenProvider.Create(user);


                return Result<LoginResponseDTO>.Success( token );
            }
        }
    }
}

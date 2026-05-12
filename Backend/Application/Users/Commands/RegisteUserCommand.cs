using Application.Abstractions.Authentication;
using Application.Users.DTOs;
using Application.Users.Mappers;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.InfoValidation;


namespace Application.Users.Commands
{
    // Command representing the intent to create a new user.
    // Using a record ensures immutability and aligns well with MediatR practices.
    public record RegisteUserCommand(UserDTO User)
    : IRequest<Result<UserEntity>>;

    // Handler responsible for processing the AddUserCommand.
    // The repository is injected via dependency injection, promoting loose coupling.
    public class RegisteUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisteUserCommand, Result<UserEntity>>
    {
        public async Task<Result<UserEntity>> Handle(RegisteUserCommand request, CancellationToken cancellationToken)
        {
            var resultUserCreation = UserMapper.FromDTO(request.User);
            if (resultUserCreation.IsFailure)
                return resultUserCreation;

            resultUserCreation.Value.PasswordHash = passwordHasher.Hash(resultUserCreation.Value.PasswordHash);


            await userRepository.Register(resultUserCreation.Value);

            return Result.Success(resultUserCreation.Value);
        }
    }

}

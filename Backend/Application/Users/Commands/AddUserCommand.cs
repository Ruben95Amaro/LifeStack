using Application.Users.DTOs;
using Application.Users.Mappers;
using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using SharedKernel.InfoValidation;


namespace Application.Users.Commands
{
    // Command representing the intent to create a new user.
    // Using a record ensures immutability and aligns well with MediatR practices.
    public record AddUserCommand(UserDTO User)
    : IRequest<Result<UserEntity>>;

    // Handler responsible for processing the AddUserCommand.
    // The repository is injected via dependency injection, promoting loose coupling.
    public class AddUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddUserCommand, Result>
    {
        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = UserMapper.FromDTO(request.User);

            var createdUser = await userRepository.AddUserAsync(user);


            return createdUser;
        }
    }

}

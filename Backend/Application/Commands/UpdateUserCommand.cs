using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands;

public record UpdateUserCommand(string UserId, UserDTO UserDTO)
    : IRequest<UserEntity>;

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, UserEntity>
{
    public async Task<UserEntity> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);

        if (user == null)
            throw new Exception("User not found");

        user.SetName(request.UserDTO.FirstName, request.UserDTO.LastName);
        user.SetEmail(request.UserDTO.Email);

        await userRepository.UpdateUserAsync(user.Id,user);


        return user;
    }
}



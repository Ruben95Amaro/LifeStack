using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands;

public record UpdateUserCommand(string UserId, UserDTO User)
    : IRequest<UserEntities>;

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, UserEntities>
{
    public async Task<UserEntities> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);

        if (user == null)
            throw new Exception("User not found");

        // 🔥 DTO → DOMAIN
        user.SetName(request.User.FirstName, request.User.LastName);
        user.SetEmail(request.User.Email);

        await userRepository.UpdateUserAsync(user.Id,user);

        return user;
    }
}


    //public record UpdateUserCommand(string UserId, UserEntities User)
    //    : IRequest<UserEntities>;
    //public class UpdateUserCommandHandler(IUserRepository userRepository)
    //    : IRequestHandler<UpdateUserCommand, UserEntities>
    //{
    //    public async Task<UserEntities> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    //    {
    //        return await userRepository.UpdateUserAsync(request.UserId, request.User);
    //    }
    //}


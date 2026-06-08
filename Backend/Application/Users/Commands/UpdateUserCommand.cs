using Application.Users.DTOs;
using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using SharedKernel.InfoValidation;

namespace Application.Users.Commands;

// Command representing the intent to update a user by Id.
// Using a record ensures immutability and aligns well with MediatR practices.
public record UpdateUserCommand(Guid Id, UserDTO UserDTO)
    : IRequest<Result>;

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);

        if (user == null)
            Result.Failure(UserErrors.NotFoundByEmail);

        var userUpdateResponse = user.Update(request.UserDTO.FirstName, request.UserDTO.LastName, request.UserDTO.Email, request.UserDTO.Password);

        if (userUpdateResponse.IsSuccess)
            await userRepository.UpdateAsync(user.Id, user);

        return userUpdateResponse;
    }
}

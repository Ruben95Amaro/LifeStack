using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    // Command representing the intent to delete a user by Id.
    // Using a record ensures immutability and aligns well with MediatR practices.
    public record DeleteUserCommand(string userId)
    : IRequest<bool>;

    internal class DeleteUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetUserByIdAsync(request.userId);

            if (user is null)
                throw new Exception("User not found");

            var result = await userRepository.DeleteUserAsync(user);

            if (!result)
                throw new Exception("Failed to delete user");

            return result;

        }
    }

}

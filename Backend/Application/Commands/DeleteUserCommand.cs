using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{

    public record DeleteUserCommand(string UserId)
    : IRequest<bool>;

    internal class DeleteUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetUserByIdAsync(request.UserId);

            if (user is null)
                throw new Exception("User not found");

            var result = await userRepository.DeleteUserAsync(user);

            if (!result)
                throw new Exception("Failed to delete user");

            return result;

        }
    }

}

using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record DeleteUserCommand(string UserId) : IRequest<bool>;

    internal class DeleteUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.DeleteUserAsync(request.UserId);
        }
    }
}

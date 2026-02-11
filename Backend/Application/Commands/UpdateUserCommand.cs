using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record UpdateUserCommand(Guid UserId, UserEntities User)
        : IRequest<UserEntities>;
    public class UpdateUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<UpdateUserCommand, UserEntities>
    {
        public async Task<UserEntities> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.UpdateUserAsync(request.UserId, request.User);
        }
    }
}

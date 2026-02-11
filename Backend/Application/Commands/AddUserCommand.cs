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
    public record AddUserCommand(UserEntities user): IRequest<UserEntities>;

    public class AddUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<AddUserCommand, UserEntities>
    {
        public async Task<UserEntities> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.AddUserAsync(request.user);
        }
    }
}

using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public record GetUserByIdQuery(string UserId) : IRequest<UserEntities>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, UserEntities>
    {
        public async Task<UserEntities> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserByIdAsync(request.UserId);
        }
    }
}

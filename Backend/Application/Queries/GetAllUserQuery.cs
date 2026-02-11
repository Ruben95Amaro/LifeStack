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
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserEntities>>;
    public class GetAllUsersQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetAllUsersQuery, IEnumerable<UserEntities>>
    {
        public async Task<IEnumerable<UserEntities>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUsers();
        }
    }
}

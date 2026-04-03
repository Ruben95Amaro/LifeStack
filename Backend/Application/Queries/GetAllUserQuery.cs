using Domain.Entities;
using Domain.Interfaces;
using MediatR;


namespace Application.Queries
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserEntity>>;
    public class GetAllUsersQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetAllUsersQuery, IEnumerable<UserEntity>>
    {
        public async Task<IEnumerable<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUsers();
        }
    }
}

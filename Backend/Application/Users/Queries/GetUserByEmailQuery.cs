using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserEntity>;

    public class GetUserByEmailQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByEmailQuery, UserEntity>
    {
        public async Task<UserEntity> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetByEmailAsync(request.Email);
        }
    }
}

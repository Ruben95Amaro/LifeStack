using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public record GetUserByIdQuery(string UserId) : IRequest<UserEntity>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserByIdAsync(request.UserId);
        }
    }
}

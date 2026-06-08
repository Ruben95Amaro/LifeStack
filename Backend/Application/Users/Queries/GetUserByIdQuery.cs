using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using SharedKernel.InfoValidation;

namespace Application.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserEntity>>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, Result<UserEntity>>
    {
        public async Task<Result<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (user == null) {
                return Result.Failure(UserErrors.NotFoundById);
            }

            return Result<UserEntity>.Success(user);
        }
    }
}

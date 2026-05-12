using Domain.Users.Entities;
using Domain.Users.Interfaces;
using MediatR;
using SharedKernel.InfoValidation;

namespace Application.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, Result>
    {
        public async Task<Result> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (user == null) {
                return Result.Failure(UserErrors.NotFoundById);
            }

            return Result.Success(user);
        }
    }
}

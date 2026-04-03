using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;


namespace Application.Commands
{
    public record AddUserCommand(UserDTO User)
    : IRequest<UserEntity>;


    public class AddUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = UserMapper.FromDTO(request.User);

            return await userRepository.AddUserAsync(user);
        }
    }

}

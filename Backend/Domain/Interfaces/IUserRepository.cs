using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity> GetUserByIdAsync(string userid);
        Task<UserEntity> AddUserAsync(UserEntity entity);
        Task<UserEntity> UpdateUserAsync(string userid, UserEntity entity);
        Task<bool> DeleteUserAsync(UserEntity entity);


    }
}

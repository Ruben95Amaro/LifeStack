using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntities>> GetUsers();
        Task<UserEntities> GetUserByIdAsync(string id);
        Task<UserEntities> AddUserAsync(UserEntities entity);
        Task<UserEntities> UpdateUserAsync(string userid, UserEntities entity);
        Task<bool> DeleteUserAsync(string userid);


    }
}

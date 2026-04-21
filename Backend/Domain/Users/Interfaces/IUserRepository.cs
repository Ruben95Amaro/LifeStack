using Domain.Users.Entities;
using SharedKernel.InfoValidation;


namespace Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        // Retrieves all users from the data store.
        // Returns a collection of domain entities.
        Task<IEnumerable<UserEntity>> GetUsers();

        // Retrieves a single user by its identifier.
        // The use of string as an ID should be consistent with the domain (e.g., GUID vs string).
        Task<UserEntity> GetUserByIdAsync(string userid);

        // Defines a contract for adding a new user to the data store.
        // Accepts a domain entity as input, keeping the repository aligned with the domain layer.
        Task<Result> AddUserAsync(UserEntity entity);

        // Updates an existing user identified by userid.
        // Accepts the updated entity data.
        Task<Result> UpdateUserAsync(string userid, UserEntity entity);

        // Deletes a user from the data store.
        // Accepts the entity to be removed.
        Task<bool> DeleteUserAsync(UserEntity entity);


    }
}

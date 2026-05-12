using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
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
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByEmailAsync(string email);

        // Defines a contract for adding a new user to the data store.
        // Accepts a domain entity as input, keeping the repository aligned with the domain layer.
        Task<Result> AddAsync(UserEntity entity);

        // Updates an existing user identified by id.
        // Accepts the updated entity data.
        Task<Result> UpdateAsync(Guid id, UserEntity entity);

        // Deletes a user from the data store.
        // Accepts the entity to be removed.
        Task<bool> DeleteAsync(UserEntity entity);

        Task<Result> Register(UserEntity entity);



    }
}

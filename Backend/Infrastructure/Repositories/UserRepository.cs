using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.ComponentModel;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        // Retrieves all users from the database.
        public async Task<IEnumerable<UserEntity>> GetUsers() => await dbContext.Users.ToListAsync();

        // Retrieves a user by its identifier.
        public async Task<UserEntity> GetUserByIdAsync(string userid) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userid);

        // Adds a new user to the database.
        // The entity is tracked by EF Core after being added.
        public async Task<Result> AddUserAsync(UserEntity entity)
        {
            var response = dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserAsync(string userid, UserEntity entity)
        {
            try
            {
                dbContext.Users.Update(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.NullValue);
            }
            
        }

        public async Task<bool> DeleteUserAsync(UserEntity entity)
        {
            dbContext.Users.Remove(entity);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}

using Domain.Users.Entities;
using Domain.Users.Interfaces;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.InfoValidation;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {


        // Retrieves all users from the database.
        public async Task<IEnumerable<UserEntity>> GetUsers() => await dbContext.Users.ToListAsync();

        // Retrieves a user by its identifier.
        public async Task<UserEntity> GetByIdAsync(Guid id) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        public async Task<UserEntity> GetByEmailAsync(string email) => await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

        // Adds a new user to the database.
        // The entity is tracked by EF Core after being added.
        public async Task<Result> AddAsync(UserEntity entity)
        {
            var response = dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(Guid id, UserEntity entity)
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

        public async Task<bool> DeleteAsync(UserEntity entity)
        {
            dbContext.Users.Remove(entity);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Result> Register(UserEntity entity)
        {
            var response = dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }


    }
}

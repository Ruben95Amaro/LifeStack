using Domain.Users.Entities;
using Domain.Users.Interfaces;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.InfoValidation;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext, UserManager<UserEntity> userManager) : IUserRepository
    {


        // Retrieves all users from the database.
        public async Task<IEnumerable<UserEntity>> GetUsers() => await userManager.Users.ToListAsync();

        // Retrieves a user by its identifier.
        public async Task<UserEntity> GetByIdAsync(Guid id) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        public async Task<UserEntity> GetByEmailAsync(string email) => await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);


        public async Task<Result> UpdateAsync(Guid id, UserEntity entity)
        {
            try
            {
                var response = await userManager.UpdateAsync(entity);

                if (!response.Succeeded) return Result.Failure(UserErrors.ConflitOnSaveUser);
                

                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.NullValue);
            }
            
        }

        public async Task<bool> DeleteAsync(UserEntity entity)
        {
            var result = await userManager.DeleteAsync(entity);


            return result.Succeeded;
        }

        // Adds a new user to the database.
        // The entity is tracked by EF Core after being added.
        public async Task<Result> Register(UserEntity entity)
        {
            var userAlreadyExist = await GetByEmailAsync(entity.Email);
            if (userAlreadyExist != null) return Result.Failure(UserErrors.EmailNotUnique);
            try
            {
                var userCreated = await userManager.CreateAsync(entity, entity.PasswordHash);

                if (!userCreated.Succeeded) return Result.Failure(UserErrors.ConflitOnSaveUser);

                //var response = await dbContext.Users.AddAsync(entity);
                //await dbContext.SaveChangesAsync();
                return Result.Success();

            }
            catch
            {
                return Result.Failure(UserErrors.ConflitOnSaveUser);
            }
                      
        }

    }
}

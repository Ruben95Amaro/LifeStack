using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(string id) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        public async Task<UserEntity> AddUserAsync(UserEntity entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntity> UpdateUserAsync(string userid, UserEntity entity)
        {
            dbContext.Users.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteUserAsync(UserEntity entity)
        {
            dbContext.Users.Remove(entity);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}

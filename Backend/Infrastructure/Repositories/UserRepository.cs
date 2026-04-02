using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<IEnumerable<UserEntities>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserEntities> GetUserByIdAsync(string id) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        public async Task<UserEntities> AddUserAsync(UserEntities entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntities> UpdateUserAsync(string userid, UserEntities entity)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userid);

            if (user is null) return entity;

            user.SetName(entity.FirstName, entity.LastName);
            user.SetEmail(entity.Email);
            user.SetUserName(entity.UserName);

            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(string userid)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userid);

            if (user is null) return false;

            dbContext.Users.Remove(user);    

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}

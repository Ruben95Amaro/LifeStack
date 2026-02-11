using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<IEnumerable<UserEntities>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserEntities> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<UserEntities> AddUserAsync(UserEntities entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntities> UpdateUserAsync(Guid userid, UserEntities entity)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userid);

            if (user is null) return entity; 

            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;

            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userid)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userid);

            if (user is null) return false;

            dbContext.Users.Remove(user);    

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}

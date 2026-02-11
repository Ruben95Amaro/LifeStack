using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntities>> GetUsers();
        Task<UserEntities> GetUserByIdAsync(Guid id);
        Task<UserEntities> AddUserAsync(UserEntities entity);
        Task<UserEntities> UpdateUserAsync(Guid userid, UserEntities entity);
        Task<bool> DeleteUserAsync(Guid userid);


    }
}

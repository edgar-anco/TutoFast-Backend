using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Persistence.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListByUserIdAsync(int userId);
        Task AddAsync(Session session);
        Task<Session> FindById(int id);
        void Update(Session session);
        void Remove(Session session);
    }
}

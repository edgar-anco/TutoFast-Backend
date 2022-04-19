using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<SessionResponse> GetByIdAsync(int id);
        Task<SessionResponse> SaveAsync(Session session,int tutorId,int userId);
        Task<IEnumerable<Session>> ListByUserIdAsync(int userId);
        Task<SessionResponse> UpdateAsync(int id, Session session);
        Task<SessionResponse> DeleteAsync(int id);
    }
}

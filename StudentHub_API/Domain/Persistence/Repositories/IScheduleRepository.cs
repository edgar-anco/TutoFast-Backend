using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Persistence.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> ListAsync();
        Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId);
        Task AddAsync(Schedule schedule);
        Task<Schedule> FindById(int id);
        void Update(Schedule schedule);
        void Remove(Schedule schedule);
    }
}

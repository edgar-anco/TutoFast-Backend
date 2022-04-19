using StudentHub_API.Domain.Services.Comunications;
using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> ListAsync();
        Task<ScheduleResponse> GetByIdAsync(int id);
        Task<ScheduleResponse> SaveAsync(Schedule schedule, int tutorId);
        Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId);
        Task<ScheduleResponse> UpdateAsync(int id, Schedule schedule);
        Task<ScheduleResponse> DeleteAsync(int id);
    }
}

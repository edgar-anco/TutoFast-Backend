using Microsoft.EntityFrameworkCore;
using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Persistence.Contexts;
using StudentHub_API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Persistence.Repositories
{
    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context)
        {
        }
        public async Task AddAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
        }

        public async Task<Schedule> FindById(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> ListAsync()
        {
            return await _context.Schedules.ToListAsync();
        }
        public async Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.Schedules
                .Where(s => s.TutorId == tutorId)
                .ToListAsync();
        }
        public void Remove(Schedule schedule)
        {
            _context.Schedules.Remove(schedule);
        }

        public void Update(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
        }
    }
}

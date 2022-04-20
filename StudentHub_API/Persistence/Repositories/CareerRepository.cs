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
    public class CareerRepository : BaseRepository, ICareerRepository
	{
		public CareerRepository(AppDbContext context) : base(context)
		{
		}

		public async Task AddAsync(Career career)
		{
			await _context.Careers.AddAsync(career);
		}

		public async Task<Career> FindById(int id)
		{
			List<Career> careers = await _context.Careers
			   .Where(career => career.Id == id)
			   .ToListAsync();
			return careers.First();
		}

		public async Task<IEnumerable<Career>> ListAsync()
		{
			return await _context.Careers
				.ToListAsync();
		}

		public void Remove(Career career)
		{
			_context.Careers.Remove(career);
		}

		public void Update(Career career)
		{
			_context.Careers.Update(career);
		}
	}
}

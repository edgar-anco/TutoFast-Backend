using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Persistence.Repositories
{
    public interface ICareerRepository
    {
        Task<IEnumerable<Career>> ListAsync();
        Task AddAsync(Career career);
        Task<Career> FindById(int id);
        void Update(Career career);
        void Remove(Career career);
    }
}

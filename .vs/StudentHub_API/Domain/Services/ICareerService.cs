using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services
{
    public interface ICareerService
    {
        Task<CareerResponse> GetByIdAsync(int id);
        Task<CareerResponse> SaveAsync(Career career);
        Task<CareerResponse> UpdateAsync(int id, Career career);
        Task<CareerResponse> DeleteAsync(int id);
    }
}

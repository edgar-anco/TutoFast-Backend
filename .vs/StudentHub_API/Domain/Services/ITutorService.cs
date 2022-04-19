using StudentHub_API.Domain.Services.Comunications;
using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services
{
    public interface ITutorService
    {
        Task<IEnumerable<Tutor>> ListAsync();
        Task<TutorResponse> GetByIdAsync(int id);
        Task<TutorResponse> SaveAsync(Tutor tutor);
        Task<TutorResponse> SaveAsync(int courseId,int userId, Tutor tutor);
        Task<IEnumerable<Tutor>> FindByCourseId(int courseId);
        Task<TutorResponse> UpdateAsync(int id, Tutor tutor);
        Task<TutorResponse> DeleteAsync(int id);
    }
}

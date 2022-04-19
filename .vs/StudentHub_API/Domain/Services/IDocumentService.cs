using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> ListAsync();
        Task<IEnumerable<Document>> ListByCareerIdAsync(int careerId);
        Task<IEnumerable<Document>> ListByCourseIdAsync(int courseId);
        Task<DocumentResponse> GetByIdAsync(int id);
        Task<DocumentResponse> SaveAsync(int userId, int careerId, int courseId, Document document);
        Task<DocumentResponse> UpdateAsync( int id, Document document);
        Task<DocumentResponse> DeleteAsync(int id);
    }
}

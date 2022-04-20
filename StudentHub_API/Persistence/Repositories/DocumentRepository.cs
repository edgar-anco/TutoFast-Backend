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
    public class DocumentRepository : BaseRepository, IDocumentRepository
    {
        public DocumentRepository(AppDbContext context) : base(context)
        {
        }
        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
        }
        public async Task<IEnumerable<Document>> ListByCourseIdAsync(int courseyId)
        {
            return await _context.Documents
                .Where(d => d.CourseId == courseyId)
                .Include(d => d.Course)
                .Include(d => d.Career)
                .Include(d => d.User)
                    .ThenInclude(d => d.Tutor)
                        .ThenInclude(d => d.Course)
                .ToListAsync();
        }
        public async Task<IEnumerable<Document>> ListByCareerIdAsync(int careerId)
        {
            return await _context.Documents
                .Where(d => d.CareerId == careerId)
                .Include(d => d.Career)
                .Include(d => d.Course)
                .Include(d => d.User)
                    .ThenInclude(d => d.Tutor)
                        .ThenInclude(d => d.Course)
                .ToListAsync();
        }
        public async Task<Document> FindById(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task<IEnumerable<Document>> ListAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public void Remove(Document document)
        {
            _context.Documents.Remove(document);
        }

        public void Update(Document document)
        {
            _context.Documents.Update(document);
        }
    }
}

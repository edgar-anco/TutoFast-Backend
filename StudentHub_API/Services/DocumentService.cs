using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Persistence.Repositories;
using StudentHub_API.Domain.Services;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IDocumentRepository documentRepository, IUnitOfWork unitOfWork)
        {
            _documentRepository = documentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DocumentResponse> DeleteAsync(int id)
        {
            var existingDocument = await _documentRepository.FindById(id);

            if (existingDocument == null)
                return new DocumentResponse("Document not found");

            try
            {
                _documentRepository.Remove(existingDocument);
                await _unitOfWork.CompleteAsync();

                return new DocumentResponse(existingDocument);
            }
            catch (Exception ex)
            {
                return new DocumentResponse($"An error ocurred while deleting the document: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Document>> ListAsync()
        {
            return await _documentRepository.ListAsync();
        }
        public async Task<IEnumerable<Document>> ListByCourseIdAsync(int courseId)
        {
            return await _documentRepository.ListByCourseIdAsync(courseId);
        }
        public async Task<IEnumerable<Document>> ListByCareerIdAsync(int careerId)
        {
            return await _documentRepository.ListByCareerIdAsync(careerId);
        }
        public async Task<DocumentResponse> GetByIdAsync(int id)
        {
            var existingDocument = await _documentRepository.FindById(id);

            if (existingDocument == null)
                return new DocumentResponse("Document not found");
            return new DocumentResponse(existingDocument);
        }

        public async Task<DocumentResponse> SaveAsync(int userId, int careerId, int courseId, Document document)
        {
            try
            {
                document.UserId = userId;
                document.CareerId = careerId;
                document.CourseId = courseId;
                await _documentRepository.AddAsync(document);
                await _unitOfWork.CompleteAsync();
                return new DocumentResponse(document);
            }
            catch (Exception e)
            {
                return new DocumentResponse($"An error ocurred while saving the document: {e.Message}");
            }
        }
        public async Task<DocumentResponse> UpdateAsync(int id, Document document)
        {
            var existingDocument = await _documentRepository.FindById(id);

            if (existingDocument == null)
                return new DocumentResponse("Document not found");

            //falta llenar los updates
            existingDocument.Name = document.Name;
            existingDocument.Description = document.Description;

            try
            {
                _documentRepository.Update(existingDocument);
                await _unitOfWork.CompleteAsync();
                return new DocumentResponse(existingDocument);
            }
            catch (Exception ex)
            {
                return new DocumentResponse($"An error ocurred while updating the document: {ex.Message}");
            }
        }
    }
}

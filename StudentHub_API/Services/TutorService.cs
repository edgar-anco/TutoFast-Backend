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
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TutorService(ITutorRepository tutorRepository, IUnitOfWork unitOfWork)
        {
            _tutorRepository = tutorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TutorResponse> DeleteAsync(int id)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");

            try
            {
                _tutorRepository.Remove(existingTutor);
                await _unitOfWork.CompleteAsync();

                return new TutorResponse(existingTutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while deleting the tutor: {ex.Message}");
            }
        }

        public async Task<TutorResponse> GetByIdAsync(int id)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");
            return new TutorResponse(existingTutor);
        }

        public async Task<IEnumerable<Tutor>> ListAsync()
        {
            return await _tutorRepository.ListAsync();
        }

        public async Task<IEnumerable<Tutor>> FindByCourseId(int courseId)
        {
            var list = await _tutorRepository.FindByCourseIdAsync(courseId);
            return list;
        }
        public async Task<TutorResponse> SaveAsync(Tutor tutor)
        {
            try
            {
                await _tutorRepository.AddAsync(tutor);
                await _unitOfWork.CompleteAsync();
                return new TutorResponse(tutor);
            }
            catch (Exception e)
            {
                return new TutorResponse($"An error ocurred while saving the tutor: {e.Message}");
            }
        }
        public async Task<TutorResponse> SaveAsync(int courseId,int userId, Tutor tutor)
        {
            try
            {
                tutor.UserId = userId;
                tutor.CourseId = courseId;
                await _tutorRepository.AddAsync(tutor);
                await _unitOfWork.CompleteAsync();
                return new TutorResponse(tutor);
            }
            catch (Exception e)
            {
                return new TutorResponse("Has ocurred an error saving the tutor " + e.Message);
            }
        }

        public async Task<TutorResponse> UpdateAsync(int id, Tutor tutor)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");

            //falta llenar los updates
            existingTutor.Description = tutor.Description;
            existingTutor.PricePerHour = tutor.PricePerHour;
            existingTutor.Url = tutor.Url;

            try
            {
                _tutorRepository.Update(existingTutor);
                await _unitOfWork.CompleteAsync();
                return new TutorResponse(existingTutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while updating the tutor: {ex.Message}");
            }
        }
    }
}

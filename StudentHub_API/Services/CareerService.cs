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
    public class CareerService : ICareerService
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CareerService(ICareerRepository careerRepository, IUnitOfWork unitOfWork)
        {
            _careerRepository = careerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CareerResponse> DeleteAsync(int id)
        {
            var existingCareer = await _careerRepository.FindById(id);

            if (existingCareer == null)
                return new CareerResponse("Career not found");

            try
            {
                _careerRepository.Remove(existingCareer);
                await _unitOfWork.CompleteAsync();

                return new CareerResponse(existingCareer);
            }
            catch (Exception ex)
            {
                return new CareerResponse($"An error ocurred while deleting the career: {ex.Message}");
            }
        }

        public async Task<CareerResponse> GetByIdAsync(int id)
        {
            var existingCareer = await _careerRepository.FindById(id);

            if (existingCareer == null)
                return new CareerResponse("Career not found");
            return new CareerResponse(existingCareer);
        }

        public async Task<IEnumerable<Career>> ListAsync()
        {
            return await _careerRepository.ListAsync();
        }

        public async Task<CareerResponse> SaveAsync(Career career)
        {
            try
            {
                await _careerRepository.AddAsync(career);
                await _unitOfWork.CompleteAsync();
                return new CareerResponse(career);
            }
            catch (Exception e)
            {
                return new CareerResponse($"An error ocurred while saving the career: {e.Message}");
            }
        }
        public async Task<CareerResponse> UpdateAsync(int id, Career career)
        {
            var existingCareer = await _careerRepository.FindById(id);

            if (existingCareer == null)
                return new CareerResponse("Career not found");

            //falta llenar los updates
            existingCareer.Name = career.Name;

            try
            {
                _careerRepository.Update(existingCareer);
                await _unitOfWork.CompleteAsync();
                return new CareerResponse(existingCareer);
            }
            catch (Exception ex)
            {
                return new CareerResponse($"An error ocurred while updating the career: {ex.Message}");
            }
        }
    }
}

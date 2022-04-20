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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseResponse> DeleteAsync(int id)
        {
            var existingCourse = await _courseRepository.FindById(id);

            if (existingCourse == null)
                return new CourseResponse("Course not found");

            try
            {
                _courseRepository.Remove(existingCourse);
                await _unitOfWork.CompleteAsync();

                return new CourseResponse(existingCourse);
            }
            catch (Exception ex)
            {
                return new CourseResponse($"An error ocurred while deleting the course: {ex.Message}");
            }
        }

        public async Task<CourseResponse> GetByIdAsync(int id)
        {
            var existingCourse = await _courseRepository.FindById(id);

            if (existingCourse == null)
                return new CourseResponse("Course not found");
            return new CourseResponse(existingCourse);
        }

        public async Task<IEnumerable<Course>> ListAsync()
        {
            return await _courseRepository.ListAsync();
        }

        public async Task<CourseResponse> SaveAsync(Course course)
        {
            try
            {
                await _courseRepository.AddAsync(course);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(course);
            }
            catch (Exception e)
            {
                return new CourseResponse($"An error ocurred while saving the course: {e.Message}");
            }
        }
        public async Task<CourseResponse> UpdateAsync(int id, Course course)
        {
            var existingCourse = await _courseRepository.FindById(id);

            if (existingCourse == null)
                return new CourseResponse("Course not found");

            //falta llenar los updates
            existingCourse.Name = course.Name;

            try
            {
                _courseRepository.Update(existingCourse);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(existingCourse);
            }
            catch (Exception ex)
            {
                return new CourseResponse($"An error ocurred while updating the course: {ex.Message}");
            }
        }
    }
}

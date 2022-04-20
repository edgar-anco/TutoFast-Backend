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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork)
        {
            _scheduleRepository = scheduleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ScheduleResponse> DeleteAsync(int id)
        {
            var existingSchedule = await _scheduleRepository.FindById(id);

            if (existingSchedule == null)
                return new ScheduleResponse("Schedule not found");

            try
            {
                _scheduleRepository.Remove(existingSchedule);
                await _unitOfWork.CompleteAsync();

                return new ScheduleResponse(existingSchedule);
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while deleting the schedule: {ex.Message}");
            }
        }

        public async Task<ScheduleResponse> GetByIdAsync(int id)
        {
            var existingSchedule = await _scheduleRepository.FindById(id);

            if (existingSchedule == null)
                return new ScheduleResponse("Schedule not found");
            return new ScheduleResponse(existingSchedule);
        }

        public async Task<IEnumerable<Schedule>> ListAsync()
        {
            return await _scheduleRepository.ListAsync();
        }
        public async Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId)
        {
            return await _scheduleRepository.ListByTutorIdAsync(tutorId);
        }
        public async Task<ScheduleResponse> SaveAsync(Schedule schedule,int tutorId)
        {
            try
            {
                schedule.TutorId = tutorId;
                await _scheduleRepository.AddAsync(schedule);
                await _unitOfWork.CompleteAsync();
                return new ScheduleResponse(schedule);
            }
            catch (Exception e)
            {
                return new ScheduleResponse($"An error ocurred while saving the schedule: {e.Message}");
            }
        }
        public async Task<ScheduleResponse> UpdateAsync(int id, Schedule schedule)
        {
            var existingSchedule = await _scheduleRepository.FindById(id);

            if (existingSchedule == null)
                return new ScheduleResponse("Schedule not found");

            //falta llenar los updates
            existingSchedule.StarDate = schedule.StarDate;
            existingSchedule.EndDate = schedule.EndDate;
            existingSchedule.Date = schedule.Date;

            try
            {
                _scheduleRepository.Update(existingSchedule);
                await _unitOfWork.CompleteAsync();
                return new ScheduleResponse(existingSchedule);
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while updating the schedule: {ex.Message}");
            }
        }
    }
}

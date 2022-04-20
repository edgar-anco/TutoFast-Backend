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
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionResponse> DeleteAsync(int id)
        {
            var existingSession = await _sessionRepository.FindById(id);

            if (existingSession == null)
                return new SessionResponse("Session not found");

            try
            {
                _sessionRepository.Remove(existingSession);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(existingSession);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while deleting the session: {ex.Message}");
            }
        }
        
        public async Task<SessionResponse> GetByIdAsync(int id)
        {
            var existingSession = await _sessionRepository.FindById(id);

            if (existingSession == null)
                return new SessionResponse("Session not found");
            return new SessionResponse(existingSession);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _sessionRepository.ListAsync();
        }
        public async Task<IEnumerable<Session>> ListByUserIdAsync(int userId)
        {
            return await _sessionRepository.ListByUserIdAsync(userId);
        }
        public async Task<SessionResponse> SaveAsync(Session session, int tutorId, int userId)
        {
            try
            {
                session.TutorId = tutorId;
                session.UserId = userId;
                await _sessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync();
                return new SessionResponse(session);
            }
            catch (Exception e)
            {
                return new SessionResponse($"An error ocurred while saving the session: {e.Message}");
            }
        }
        public async Task<SessionResponse> UpdateAsync(int id, Session session)
        {
            var existingSession = await _sessionRepository.FindById(id);

            if (existingSession == null)
                return new SessionResponse("Session not found");

            //falta llenar los updates
            existingSession.Name = session.Name;


            try
            {
                _sessionRepository.Update(existingSession);
                await _unitOfWork.CompleteAsync();
                return new SessionResponse(existingSession);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while updating the session: {ex.Message}");
            }
        }
    }
}

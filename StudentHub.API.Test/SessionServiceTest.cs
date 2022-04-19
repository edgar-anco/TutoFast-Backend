using FluentAssertions;
using Moq;
using NUnit.Framework;
using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Persistence.Repositories;
using StudentHub_API.Domain.Services.Comunications;
using StudentHub_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.API.Test
{
    class SessionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoSessionFoundReturnsSessionNotFoundResponse()
        {
            // Arrange
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var sessionId = 1;
            mockSessionRepository.Setup(r => r.FindById(sessionId))
                .Returns(Task.FromResult<Session>(null));

            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionResponse result = await service.GetByIdAsync(sessionId);
            var message = result.Message;

            // Assert
            message.Should().Be("Session not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenSessionFoundReturnsSuccess()
        {
            // Arrange
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var sessionId = 1;
            Session c = new()
            {
                Id = 1,
                Name = "Programación Basica"
            };

            mockSessionRepository.Setup(r => r.FindById(sessionId))
                .Returns(Task.FromResult<Session>(c));
            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionResponse result = await service.GetByIdAsync(sessionId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<ISessionRepository> GetDefaultISessionRepositoryInstance()
        {
            return new Mock<ISessionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

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
    class TutorServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoTutorFoundReturnsTutorNotFoundResponse()
        {
            // Arrange
            var mockTutorRepository = GetDefaultITutorRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var tutorId = 1;
            mockTutorRepository.Setup(r => r.FindById(tutorId))
                .Returns(Task.FromResult<Tutor>(null));

            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.GetByIdAsync(tutorId);
            var message = result.Message;

            // Assert
            message.Should().Be("Tutor not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenTutorFoundReturnsSuccess()
        {
            // Arrange
            var mockTutorRepository = GetDefaultITutorRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var tutorId = 1;
            Tutor c = new()
            {
                Id = 1,
                Description = "Pasé con honores los cursos de progrmación en mi universidad",
                PricePerHour = 20
            };

            mockTutorRepository.Setup(r => r.FindById(tutorId))
                .Returns(Task.FromResult<Tutor>(c));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.GetByIdAsync(tutorId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<ITutorRepository> GetDefaultITutorRepositoryInstance()
        {
            return new Mock<ITutorRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

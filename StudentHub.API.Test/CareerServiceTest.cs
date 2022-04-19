using NUnit.Framework;
using System.Threading.Tasks;
using StudentHub_API.Domain.Models;
using StudentHub_API.Services;
using StudentHub_API.Domain.Services.Comunications;
using StudentHub_API.Domain.Persistence.Repositories;
using FluentAssertions;
using Moq;

namespace StudentHub.API.Test
{
    public class CareerServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCareerFoundReturnsCareerNotFoundResponse()
        {
            // Arrange
            var mockCareerRepository = GetDefaultICareerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var careerId = 1;
            mockCareerRepository.Setup(r => r.FindById(careerId))
                .Returns(Task.FromResult<Career>(null));

            var service = new CareerService(mockCareerRepository.Object, mockUnitOfWork.Object);

            // Act
            CareerResponse result = await service.GetByIdAsync(careerId);
            var message = result.Message;

            // Assert
            message.Should().Be("Career not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenCareerFoundReturnsSuccess()
        {
            // Arrange
            var mockCareerRepository = GetDefaultICareerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var careerId = 1;
            Career c = new()
            {
                Id = 1,
                Name = "Ing. Software"
            };

            mockCareerRepository.Setup(r => r.FindById(careerId))
                .Returns(Task.FromResult<Career>(c));
            var service = new CareerService(mockCareerRepository.Object, mockUnitOfWork.Object);

            // Act
            CareerResponse result = await service.GetByIdAsync(careerId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<ICareerRepository> GetDefaultICareerRepositoryInstance()
        {
            return new Mock<ICareerRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
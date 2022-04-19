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
    class ScheduleServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoScheduleFoundReturnsScheduleNotFoundResponse()
        {
            // Arrange
            var mockScheduleRepository = GetDefaultIScheduleRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var scheduleId = 1;
            mockScheduleRepository.Setup(r => r.FindById(scheduleId))
                .Returns(Task.FromResult<Schedule>(null));

            var service = new ScheduleService(mockScheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            ScheduleResponse result = await service.GetByIdAsync(scheduleId);
            var message = result.Message;

            // Assert
            message.Should().Be("Schedule not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenScheduleFoundReturnsSuccess()
        {
            // Arrange
            var mockScheduleRepository = GetDefaultIScheduleRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var scheduleId = 1;
            Schedule c = new()
            {
                Id = 1,
                StarDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
                Date = DateTime.MinValue
            };

            mockScheduleRepository.Setup(r => r.FindById(scheduleId))
                .Returns(Task.FromResult<Schedule>(c));
            var service = new ScheduleService(mockScheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            ScheduleResponse result = await service.GetByIdAsync(scheduleId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<IScheduleRepository> GetDefaultIScheduleRepositoryInstance()
        {
            return new Mock<IScheduleRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

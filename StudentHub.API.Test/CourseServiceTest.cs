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
    class CourseServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCourseFoundReturnsCourseNotFoundResponse()
        {
            // Arrange
            var mockCourseRepository = GetDefaultICourseRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var courseId = 1;
            mockCourseRepository.Setup(r => r.FindById(courseId))
                .Returns(Task.FromResult<Course>(null));

            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

            // Act
            CourseResponse result = await service.GetByIdAsync(courseId);
            var message = result.Message;

            // Assert
            message.Should().Be("Course not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenCourseFoundReturnsSuccess()
        {
            // Arrange
            var mockCourseRepository = GetDefaultICourseRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var courseId = 1;
            Course c = new()
            {
                Id = 1,
                Name = "Programación1"
            };

            mockCourseRepository.Setup(r => r.FindById(courseId))
                .Returns(Task.FromResult<Course>(c));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

            // Act
            CourseResponse result = await service.GetByIdAsync(courseId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<ICourseRepository> GetDefaultICourseRepositoryInstance()
        {
            return new Mock<ICourseRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

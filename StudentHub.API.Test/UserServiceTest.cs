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
    class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoUserFoundReturnsUserNotFoundResponse()
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));

            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;

            // Assert
            message.Should().Be("User not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenUserFoundReturnsSuccess()
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var userId = 1;
            User c = new()
            {
                Id = 1,
                Name = "Pepe",
                Email = "PepeEtesech@gmail.com",
                Password = "Pepecontraseña",
                Accepted = false
            };

            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(c));
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            UserResponse result = await service.GetByIdAsync(userId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

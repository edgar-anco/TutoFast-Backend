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
    class DocumentServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoDocumentFoundReturnsDocumentNotFoundResponse()
        {
            // Arrange
            var mockDocumentRepository = GetDefaultIDocumentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var documentId = 1;
            mockDocumentRepository.Setup(r => r.FindById(documentId))
                .Returns(Task.FromResult<Document>(null));

            var service = new DocumentService(mockDocumentRepository.Object, mockUnitOfWork.Object);

            // Act
            DocumentResponse result = await service.GetByIdAsync(documentId);
            var message = result.Message;

            // Assert
            message.Should().Be("Document not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenDocumentFoundReturnsSuccess()
        {
            // Arrange
            var mockDocumentRepository = GetDefaultIDocumentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var documentId = 1;
            Document c = new()
            {
                Id = 1,
                Name = "Variables",
                Description = "Apuntes donde encontrarás todo lo necesario para comenzar a programar"
                
            };

            mockDocumentRepository.Setup(r => r.FindById(documentId))
                .Returns(Task.FromResult<Document>(c));
            var service = new DocumentService(mockDocumentRepository.Object, mockUnitOfWork.Object);

            // Act
            DocumentResponse result = await service.GetByIdAsync(documentId);
            var success = result.Success;

            // Assert
            success.Should().Be(true);
        }

        private Mock<IDocumentRepository> GetDefaultIDocumentRepositoryInstance()
        {
            return new Mock<IDocumentRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

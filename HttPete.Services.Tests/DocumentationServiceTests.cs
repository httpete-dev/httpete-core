using Abp.Application.Features;
using AutoFixture;
using AutoFixture.Kernel;
using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using HttPete.Services.DTOs;
using HttPete.Services.Services;
using HttPete.Services.Services.Models;
using Moq;

namespace HttPete.Services.Tests
{
    [TestClass]
    public class DocumentationServiceTests
    {
        private Mock<IDocumentationRepository> _mockRepository;
        private IDocumentationService _service;
        private Fixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDocumentationRepository>();
            _service = new DocumentationService(_mockRepository.Object);
            _fixture = new Fixture();
            _fixture.Register<IEnumerable<DocumentDto>>(() => Enumerable.Empty<DocumentDto>());
        }

        [TestMethod]
        public async Task Get_WhenDocumentExists_ReturnsSuccessResult()
        {
            // Arrange
            int documentId = 1;
            var document = new Document { Id = documentId, Title = "Test Document" };
            _mockRepository.Setup(r => r.GetById(documentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(document);

            // Act
            var result = await _service.Get(documentId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(documentId, result.Value.Id);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task Get_WhenDocumentDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            int documentId = 1;
            _mockRepository.Setup(r => r.GetById(documentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Document)null);

            // Act
            var result = await _service.Get(documentId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.AreEqual(ResultStatus.NotFound, result.Status);
            Assert.AreEqual($"Document with the id {documentId} doesn't exist.", result.Error);
        }

        [TestMethod]
        public async Task GetRootDocuments_WhenDocumentsExist_ReturnsSuccessResult()
        {
            // Arrange
            int organizationId = 1;
            int workspaceId = 1;
            var documents = new List<Document>
            {
                new Document { Id = 1, Title = "Doc 1" },
                new Document { Id = 2, Title = "Doc 2" }
            };

            _mockRepository.Setup(r => r.GetRootDocuments(
                organizationId,
                workspaceId,
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(documents);

            // Act
            var result = await _service.GetRootDocuments(organizationId, workspaceId, true);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value.Count);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task GetRootDocuments_WhenNoDocuments_ReturnsNotFoundResult()
        {
            // Arrange
            int organizationId = 1;
            int workspaceId = 1;
            _mockRepository.Setup(r => r.GetRootDocuments(
                organizationId,
                workspaceId,
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Document>());

            // Act
            var result = await _service.GetRootDocuments(organizationId, workspaceId, true);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.AreEqual(ResultStatus.NotFound, result.Status);
            Assert.AreEqual($"No documents found for organization {organizationId} and workspace {workspaceId}.", result.Error);
        }

        [TestMethod]
        public async Task Update_WhenDocumentExists_ReturnsSuccessResult()
        {
            // Arrange
            var documentDto = _fixture.Build<DocumentDto>()
                .With(d => d.Id, 1)
                .With(d => d.Title, "Updated Document")
                .Create();
            var existingDocument = new Document { Id = 1, Title = "Original Document" };
            var updatedDocument = new Document { Id = 1, Title = "Updated Document" };

            _mockRepository.Setup(r => r.GetById(documentDto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingDocument);
            _mockRepository.Setup(r => r.Update(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedDocument);

            // Act
            var result = await _service.Update(documentDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(documentDto.Id, result.Value.Id);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task Update_WhenDocumentDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var documentDto = _fixture.Create<DocumentDto>();
            _mockRepository.Setup(r => r.GetById(documentDto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Document)null);

            // Act
            var result = await _service.Update(documentDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.AreEqual(ResultStatus.NotFound, result.Status);
            Assert.AreEqual($"Document with the id {documentDto.Id} doesn't exist.", result.Error);
        }

        [TestMethod]
        public async Task Create_ReturnsSuccessResult()
        {
            // Arrange
            var createDocumentDto = _fixture.Build<CreateDocumentDto>()
                .With(d => d.Title, "New Document")
                .Create();
            var createdDocument = new Document { Id = 1, Title = "New Document" };

            _mockRepository.Setup(r => r.Add(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdDocument);

            // Act
            var result = await _service.Create(createDocumentDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(createdDocument.Id, result.Value.Id);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }
    }
}

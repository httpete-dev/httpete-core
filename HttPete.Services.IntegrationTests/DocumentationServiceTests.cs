using AutoFixture;
using HttPete.Infrastructure.IntegrationTests.Utils;
using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Infrastructure.Persistence.SQLite.Repositories;
using HttPete.Model.Tenants;
using HttPete.Services.DTOs;
using HttPete.Services.Services;
using HttPete.Services.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HttPete.Services.IntegrationTests
{
    [TestClass]
    public sealed class DocumentationServiceTests
    {
        private HttPeteDbContextFixture _dbContext;
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new HttPeteDbContextFixture();
            _fixture = new Fixture();
            _fixture.Register<IEnumerable<DocumentDto>>(() => Enumerable.Empty<DocumentDto>());
            _fixture.Register<IEnumerable<Document>>(() => Enumerable.Empty<Document>());
        }

        [TestMethod]
        public async Task Create_ReturnSuccessResult()
        {
            //Arrange
            var documentationRepository = new DocumentationRepository(_dbContext.Context);
            var documentationService = new DocumentationService(documentationRepository);
            var documentDto = _fixture.Create<CreateDocumentDto>();

            //Act
            var result = await documentationService.Create(documentDto);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            AssertDocumentFieldsMapping(documentDto, result.Value);
        }

        //TODO fix test
        [TestMethod]
        public async Task Update_ReturnSuccessResult()
        {
            //Arrange
            var documentationRepository = new DocumentationRepository(_dbContext.Context);
            var documentationService = new DocumentationService(documentationRepository);
            var document = _fixture.Create<Document>();
            await _dbContext.Context.Documents.AddAsync(document);
            await _dbContext.Context.SaveChangesAsync();
            var documentUpdateDto = new DocumentDto(
                Id: document.Id,
                OrganizationId: document.OrganizationId,
                WorkspaceId: document.WorkspaceId,
                ParentId: document.ParentId,
                Title: "I was happily updated :)",
                Text: document.Text,
                LastEditById: document.LastEditById,
                LastEdited: document.LastEdited,
                Created: document.Created,
                AuthorId: document.AuthorId,
                RelatedEndpointsIds: document.RelatedEndpointsIds.ToList(),
                Children: new List<DocumentDto>(),
                EndpointId: document.EndpointId
            );

            //Act
            var result = await documentationService.Update(documentUpdateDto);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            AssertDocumentFieldsMapping(documentUpdateDto, result.Value, "I was happily updated :)");
        }

        //TODO add tests for GET

        private void AssertDocumentFieldsMapping(DocumentDto documentDto1, DocumentDto documentDto2, string title)
        {
            Assert.AreEqual(documentDto1.Id, documentDto2.Id);
            Assert.AreEqual(documentDto1.OrganizationId, documentDto2.OrganizationId);
            Assert.AreEqual(documentDto1.WorkspaceId, documentDto2.WorkspaceId);
            Assert.AreEqual(documentDto1.ParentId, documentDto2.ParentId);
            Assert.AreEqual(documentDto1.Text, documentDto2.Text);
            Assert.AreEqual(documentDto1.AuthorId, documentDto2.AuthorId);
            Assert.AreEqual(documentDto1.EndpointId, documentDto2.EndpointId);
            Assert.AreEqual(documentDto1.Title, title);
        }

        private void AssertDocumentFieldsMapping(CreateDocumentDto createDto, DocumentDto documentDto)
        {
            Assert.IsTrue(documentDto.Id > 0);
            Assert.AreEqual(createDto.OrganizationId, documentDto.OrganizationId);
            Assert.AreEqual(createDto.WorkspaceId, documentDto.WorkspaceId);
            Assert.AreEqual(createDto.ParentId, documentDto.ParentId);
            Assert.AreEqual(createDto.Title, documentDto.Title);
            Assert.AreEqual(createDto.Text, documentDto.Text);
            Assert.AreEqual(createDto.AuthorId, documentDto.AuthorId);
            Assert.AreEqual(createDto.EndpointId, documentDto.EndpointId);
            CollectionAssert.AreEqual(createDto.RelatedEndpointsIds.ToList(), documentDto.RelatedEndpointsIds.ToList());
        }
    }
}

using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Application.Services
{
    public interface ICollectionService
    {
        Task<Collection> Create(Collection collection, CancellationToken cancellationToken);

        Task<Collection?> Delete(int collectionId, CancellationToken cancellationToken);

        Task<Collection?> Update(Collection collection, CancellationToken cancellationToken);

        Task<Collection?> GetCollection(int collectionId, CancellationToken cancellationToken);
    }

    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _repository;

        public CollectionService(ICollectionRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));

            _repository = repository;
        }

        public async Task<Collection> Create(Collection collection, CancellationToken cancellationToken)
        => await _repository.Add(collection, cancellationToken);

        public async Task<Collection?> Delete(int collectionId, CancellationToken cancellationToken)
            => await _repository.Delete(collectionId, cancellationToken);

        public async Task<Collection?> Update(Collection collection, CancellationToken cancellationToken)
            => await _repository.Update(collection, cancellationToken);

        public async Task<Collection?> GetCollection(int collectionId, CancellationToken cancellationToken)
            => await _repository.GetById(collectionId, cancellationToken);
    }
}

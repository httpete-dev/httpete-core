using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Domain.Interfaces.Repositories
{
    public interface ICollectionsRepository
    {
        Task<IReadOnlyList<Collection>> GetWorkspaceCollections(int workspaceId, CancellationToken cancellationToken);
        Task<Collection?> GetCollection(int collectionId, CancellationToken cancellationToken);
        Task<Collection> Add(Collection collection, CancellationToken cancellationToken);
        Task<Collection?> Update(Collection collection, CancellationToken cancellationToken);
        Task<Collection?> Delete(int collectionId, CancellationToken cancellationToken);
    }
}

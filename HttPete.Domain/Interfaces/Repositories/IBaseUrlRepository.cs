using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Domain.Interfaces.Repositories
{
    public interface IBaseUrlRepository
    {
        Task<IEnumerable<BaseUrl>> GetByWorkspaceIdAsync(int workspaceId, CancellationToken cancellationToken = default);
        Task<BaseUrl> AddAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> UpdateAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}

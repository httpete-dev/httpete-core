using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services
{
    public interface IBaseUrlService
    {
        Task<IEnumerable<BaseUrl>> GetBaseUrlsForWorkspaceAsync(int workspaceId, CancellationToken cancellationToken = default);
        Task<BaseUrl> AddBaseUrlAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> UpdateBaseUrlAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> DeleteBaseUrlAsync(int id, CancellationToken cancellationToken = default);
    }

    public class BaseUrlService : IBaseUrlService
    {
        private readonly IBaseUrlRepository _baseUrlRepository;

        public BaseUrlService(IBaseUrlRepository baseUrlRepository)
        {
            _baseUrlRepository = baseUrlRepository;
        }

        public async Task<IEnumerable<BaseUrl>> GetBaseUrlsForWorkspaceAsync(int workspaceId, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.GetByWorkspaceIdAsync(workspaceId, cancellationToken);
        }

        public async Task<BaseUrl> AddBaseUrlAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.AddAsync(baseUrl, cancellationToken);
        }

        public async Task<BaseUrl> UpdateBaseUrlAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.UpdateAsync(baseUrl, cancellationToken);
        }

        public async Task<BaseUrl> DeleteBaseUrlAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

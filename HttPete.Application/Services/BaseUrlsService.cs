using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Application.Services
{
    public interface IBaseUrlService
    {
        Task<IEnumerable<BaseUrl>> GetBaseUrlsForWorkspace(int workspaceId, CancellationToken cancellationToken = default);
        Task<BaseUrl> AddBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> UpdateBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default);
        Task<BaseUrl> DeleteBaseUrl(int id, CancellationToken cancellationToken = default);
    }

    public class BaseUrlService : IBaseUrlService
    {
        private readonly IBaseUrlRepository _baseUrlRepository;

        public BaseUrlService(IBaseUrlRepository baseUrlRepository)
        {
            _baseUrlRepository = baseUrlRepository;
        }

        public async Task<IEnumerable<BaseUrl>> GetBaseUrlsForWorkspace(int workspaceId, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.GetByWorkspaceIdAsync(workspaceId, cancellationToken);
        }

        public async Task<BaseUrl> AddBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.Add(baseUrl, cancellationToken);
        }

        public async Task<BaseUrl> UpdateBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.Update(baseUrl, cancellationToken);
        }

        public async Task<BaseUrl> DeleteBaseUrl(int id, CancellationToken cancellationToken = default)
        {
            return await _baseUrlRepository.Delete(id, cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services.DTOs
{
    public record DocumentDto(
        int Id,
        int OrganizationId,
        int WorkspaceId,
        int? ParentId,
        string Title,
        string Text,
        int AuthorId,
        int LastEditById,
        DateTime Created,
        DateTime LastEdited,
        IReadOnlyCollection<int> RelatedEndpointsIds,
        IReadOnlyCollection<DocumentDto> Children,
        int? EndpointId
    );
}

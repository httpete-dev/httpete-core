using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services.DTOs
{
    public record CreateDocumentDto(
        int OrganizationId,
        int WorkspaceId,
        int? ParentId,
        string Title,
        string Text,
        int AuthorId,
        int? EndpointId,
        IReadOnlyCollection<int> RelatedEndpointsIds
    );
}

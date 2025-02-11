using HttPete.Model.Tenants;
using Mapster;

namespace HttPete.Services.DTOs.Mappers
{
    /// <summary>
    /// Map Document to DocumentDto and vice versa.
    /// Only fields with different names need to be mapped.
    /// </summary>
    public class DocumentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Document, DocumentDto>();

            config.NewConfig<DocumentDto, Document>();
        }
    }
}

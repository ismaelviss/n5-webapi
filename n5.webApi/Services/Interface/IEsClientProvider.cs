using n5.webApi.Models;
using n5.webApi.Models.dto;
using Nest;

namespace n5.webApi.Services.Interface
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();

        IndexResponse SaveEntity(PermissionDto permission);
    }
}
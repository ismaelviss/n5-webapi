using System;
using Microsoft.Extensions.Configuration;
using n5.webApi.Models.dto;
using n5.webApi.Services.Interface;
using Nest;

namespace n5.webApi.Services.Impl
{
    public class EsClientProvider : IEsClientProvider
    {
        private readonly IConfiguration _configuration;
        private ElasticClient _client;

        public EsClientProvider(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public ElasticClient GetClient()
        {
            if (_client != null)
            {
                return _client;
            }

            InitClient();
            return _client;
        }

        public IndexResponse SaveEntity(PermissionDto permission)
        {
            return GetClient().IndexDocument(permission);
        }

        private void InitClient()
        {
            var node = new Uri(_configuration["EsUrl"]);
            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex(_configuration["DefaultIndex"]));
        }
    }
}
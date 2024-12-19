using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elasticsearch_Using_Elastic.Clients.Elasticsearch.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Elasticsearch_Using_Elastic.Clients.Elasticsearch.Services
{
    public interface ISearchService
    {
        Task<AirbnbData?> Get(string id);

        Task<SearchResponse<AirbnbData>> SearchDocumentsByName(int pageNumber, int maxSize, string name);

        Task<bool> AddDoc(AirbnbData data);

        Task<bool> UpdateDoc(string id, AirbnbData data);

        Task<bool> DeleteDoc(string id);
    }

    public class SearchService : ISearchService
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private string _defaultIndex;
        public SearchService(ElasticsearchClient elasticsearchClient, string defaultIndex)
        {
            _elasticsearchClient = elasticsearchClient;
            _defaultIndex = defaultIndex;
        }

        public async Task<AirbnbData?> Get(string id)
        {
            var response = await _elasticsearchClient.GetAsync<AirbnbData>(new Id(id), idx => idx.Index(_defaultIndex));
            
            return response.Source;
        }

        public async Task<SearchResponse<AirbnbData>> SearchDocumentsByName(int pageNumber, int maxSize, string name)
        {
            var searchResponse = await _elasticsearchClient.SearchAsync<AirbnbData>(s => s
                                .Index(_defaultIndex)
                                    .Query(q => q
                                        .Match(m => m
                                        .Field(f => f.name).Query(name)
                                    )
                                )
                                .From(pageNumber)
                                .Size(maxSize)
                            );

            return searchResponse;
        }

        public async Task<bool> AddDoc(AirbnbData data)
        {
           var indexResponse = await _elasticsearchClient.IndexAsync<AirbnbData>(data);

           return indexResponse.IsSuccess();
        }

        public async Task<bool> UpdateDoc(string id, AirbnbData data)
        {
            var indexResponse = await _elasticsearchClient.UpdateAsync<AirbnbData, AirbnbData>(data, new Id(id));

            return indexResponse.IsSuccess();
        }

        public async Task<bool> DeleteDoc(string id)
        {
            var indexResponse = await _elasticsearchClient.DeleteAsync<AirbnbData>(new Id(id));

            return indexResponse.IsSuccess();
        }
    }
}

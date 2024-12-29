using Elastic.Clients.Elasticsearch;
using Elasticsearch_Using_Elastic.Clients.Elasticsearch.Models;

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

        /// <summary>
        /// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-get.html
        /// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AirbnbData?> Get(string id)
        {
            var searchResponse = await _elasticsearchClient.GetAsync<AirbnbData>(new Id(id), config =>
            {
                config.Index(_defaultIndex);
            });
            return searchResponse.Source;
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
            var indexResponse = await _elasticsearchClient.UpdateAsync<AirbnbData, AirbnbData>(
                    _defaultIndex,
                     new Id(id),
                     update => update
                    .Doc(data)
                    .Refresh(Refresh.True)
                );

            return indexResponse.IsSuccess();
        }

        public async Task<bool> DeleteDoc(string id)
        {
            var indexResponse = await _elasticsearchClient.DeleteAsync<AirbnbData>(new Id(id));

            return indexResponse.IsSuccess();
        }
    }
}

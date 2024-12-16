using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elasticsearch_Using_Elastic.Clients.Elasticsearch.Models;

namespace Elasticsearch_Using_Elastic.Clients.Elasticsearch.Services
{
    public interface ISearchService
    {
        Task<SearchResponse<AirbnbData>> SearchDocumentsByName(int pageNumber, int maxSize, string name);
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

        public async Task<SearchResponse<AirbnbData>> SearchDocumentsByName(int pageNumber, int maxSize, string name)
        {
            var searchResponse = await _elasticsearchClient.SearchAsync<AirbnbData>(s => s
                                .Index(_defaultIndex)
                                    .Query(q => q
                                        .Match(m => m
                                        .Field(f => f.name).Query(name)
                                    )
                                )
                                .From(0)
                                .Size(100)
                            );

            return searchResponse;
        }
    }
}

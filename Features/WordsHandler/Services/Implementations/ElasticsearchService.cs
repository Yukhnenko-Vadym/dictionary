using Elastic.Clients.Elasticsearch;

public class ElasticsearchService : IElasticsearchService<WordElastic>
{
    private readonly ElasticsearchClient _client;
    private readonly string _defaultIndex;

    public ElasticsearchService(ElasticsearchClient client)
    {
        _defaultIndex = "words-term-index";

        _client = client;
    }

    public string GetDefaultIndex() => _defaultIndex;

    public async Task CreateINdexIfNotExistsAsync(string indexName)
    {
        if (! (await _client.Indices.ExistsAsync(indexName)).Exists)
            await _client.Indices.CreateAsync(indexName);
    }

    public async Task<IndexResponse> CreateDocumentAsync(WordElastic document)
    {
        var response = await _client.IndexAsync(document);
        return response;
    }
  
    public async Task<DeleteResponse> DeleteDocumentAsync(string id)
    {
        var response = await _client.DeleteAsync<WordElastic>(id);
        return response;
    }

    public async Task<IEnumerable<WordElastic>> SearchDocumentsAsync(Func<SearchRequestDescriptor<WordElastic>, SearchRequestDescriptor<WordElastic>> searchDescriptor)
    {
        var request = searchDescriptor(new SearchRequestDescriptor<WordElastic>());
        var response = await _client.SearchAsync<WordElastic>(request);
        return response.Documents;
    }

    public async Task<UpdateResponse<WordElastic>> UpdateDocumentAsync(string id, Func<UpdateRequest<WordElastic, object>, UpdateRequest<WordElastic, object>> updateDescriptor)
    {
        var request = updateDescriptor(new UpdateRequest<WordElastic, object>(this.GetDefaultIndex(),id));
        var response = await _client.UpdateAsync<WordElastic, object>(request);
        return response;
    }
}

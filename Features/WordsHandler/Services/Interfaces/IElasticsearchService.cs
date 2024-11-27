using Elastic.Clients.Elasticsearch;
public interface IElasticsearchService<T> where T : class
{
    Task CreateINdexIfNotExistsAsync(string indexName);
    Task<IndexResponse> CreateDocumentAsync(T document);
    Task<DeleteResponse> DeleteDocumentAsync(string id);
    Task<IEnumerable<T>> SearchDocumentsAsync(Func<SearchRequestDescriptor<WordElastic>, SearchRequestDescriptor<WordElastic>> searchDescriptor);
    Task<UpdateResponse<T>> UpdateDocumentAsync(string id, Func<UpdateRequest<WordElastic, object>, UpdateRequest<WordElastic, object>> updateDescriptor);
}
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using DictionaryApp.Data;

[ApiController]
[Route("api/[controller]")]
public class SearchElasticController: ControllerBase
{
    private readonly DbContext _dbContext;
     private readonly ElasticsearchClient _client;

    public SearchElasticController(DbContext dbContext)
    {
        _dbContext = dbContext;
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("words-term-index");

        _client = new ElasticsearchClient(settings);
    }

    [HttpGet]
    public async Task IndexDbData()
    {
        var allData = await _dbContext.Words.Find(FilterDefinition<Word>.Empty).ToListAsync();

        var indexExists = await _client.Indices.ExistsAsync("words-term-index");
        if (indexExists.Exists)
        {
            // Delete the index
            var deleteResponse = await _client.Indices.DeleteAsync("words-term-index");
            if (!deleteResponse.IsValidResponse)
            {
                throw new Exception($"Failed to delete index: {deleteResponse.ElasticsearchServerError}");
            }
        }
        var createResponse = await _client.Indices.CreateAsync("words-term-index");
        if (!createResponse.IsValidResponse)
        {
            throw new Exception($"Failed to create index: {createResponse.ElasticsearchServerError}");
        }
        
        var batches = allData
            .Select((doc, index) => new { doc, index })
            .GroupBy(x => x.index / 100)
            .Select(g => g.Select(x => new WordElastic(x.doc.Id, x.doc.Term, x.doc.Definition)))
            .ToList();

        foreach (var batch in batches)
        {
            // Bulk insert the batch
            var bulkResponse = await _client.BulkAsync(b => b
                .IndexMany(batch)
            );
        }
    }

    [HttpGet]
    [Route("index/")]
    public async Task<bool> Index(){
        var indexExists = await _client.Indices.ExistsAsync("words-term-index");
        return indexExists.Exists;
    }
}
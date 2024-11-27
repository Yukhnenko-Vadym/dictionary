using Elastic.Clients.Elasticsearch;
using MongoDB.Driver;

using DictionaryApp.Data;

public class WordsRepository : IWordsRepository<Word>
{
    private readonly DbContext _dbContext;
    private readonly IElasticsearchService<WordElastic> _elasticsearchService;
    public WordsRepository(DbContext dbContext, IElasticsearchService<WordElastic> elasticsearchService)
    {
        _dbContext = dbContext;
        _elasticsearchService = elasticsearchService;
    }

    public async Task<Word> GetById(string id)
    {
        var result = await _dbContext.Words.Find(w => w.Id == id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IList<Word>> GetByTerm(string term)
    {
        // var result = await _dbContext.Words.Find(w => w.Term == term).FirstOrDefaultAsync();
        // return result;  

        var searchResult = await _elasticsearchService.SearchDocumentsAsync(s => s
            .Query(q => q.Match(m => m.Field(f => f.Term).Query(term).Fuzziness(new Fuzziness("AUTO"))))
        );

        var wordIds = searchResult.Select(r => r.Id).ToList();

        if (!wordIds.Any()) {
            return [];
        }

        var words = await _dbContext.Words.Find(w => wordIds.Contains(w.Id)).ToListAsync();

        return words.OrderBy(w => wordIds.IndexOf(w.Id)).ToList();
    }

    public async Task<Word> CreateWord(Word newWord)
    {
        if (newWord == null)
        {
            throw new NullReferenceException("Can't add the null word");
        }

        await _dbContext.Words.InsertOneAsync(newWord);

        var elasticWord = MapToElastic(newWord);
        await _elasticsearchService.CreateDocumentAsync(elasticWord);

        return newWord;
    }

    public async Task<Word> UpdateWord(string id,Word updWord)
    {
        if (updWord == null)
        {
            throw new NullReferenceException("Can't update the null word");
        }

        var existingWord = await GetById(id);
        if (existingWord == null)
        {
            throw new Exception($"No word found with id {id}");
        }

        existingWord.Term = updWord.Term;
        existingWord.Definition = updWord.Definition;

        var updateResult = await _dbContext.Words.ReplaceOneAsync(
            w => w.Id == id, 
            existingWord
        );

        if (updateResult.ModifiedCount > 0)
        {
            var elasticWord = MapToElastic(existingWord);
            await _elasticsearchService.UpdateDocumentAsync(id, u => 
            {
                u.Doc = elasticWord;
                return u;
            });
            return existingWord;
        }

        throw new Exception("Failed to update the word. It may not exist.");
    }

    public async Task DeleteWord(string id)
    {
        if (id == null || id == "")
        {
            throw new ArgumentNullException("Argument field is null or empty");
        }
        await _dbContext.Words.DeleteOneAsync(w => w.Id == id);

        await _elasticsearchService.DeleteDocumentAsync(id);
    }

    private WordElastic MapToElastic(Word word)
    {
        if (word == null) throw new ArgumentNullException(nameof(word));
        
        return new WordElastic(word.Id, word.Term, word.Definition);
    }

    private Word MapToWord(WordElastic elasticWord)
    {
        if (elasticWord == null) throw new ArgumentNullException(nameof(elasticWord));
        
        return new Word(elasticWord.Term, elasticWord.Definition);
    }
}
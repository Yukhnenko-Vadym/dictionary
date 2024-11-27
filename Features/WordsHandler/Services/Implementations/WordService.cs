

public class WordService : IWordService<Word>
{
    private readonly IWordsRepository<Word> _repository;
    public WordService(IWordsRepository<Word> repository)
    {
        _repository = repository;
    }

    public async Task<Word> GetWordById(string id)
    {
        return await _repository.GetById(id);
    }

    public async Task<IList<Word>> GetWordByTerm(string term)
    {
        return await _repository.GetByTerm(term);
    }

    public async Task<Word> CreateWord(Word newWord)
    {
        return await _repository.CreateWord(new Word(newWord.Term, newWord.Definition));
    }

    public async Task<Word> UpdateWord(string id, Word updWord)
    {
        return await _repository.UpdateWord(id, updWord);
    }

    public async Task DeleteWord(string id)
    {
        await _repository.DeleteWord(id);
    }  
}
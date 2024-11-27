public interface IWordService<WordModel> where WordModel: class
{
    public Task<WordModel> GetWordById(string id);
    public Task<IList<WordModel>> GetWordByTerm(string term);
    public Task<WordModel> CreateWord(WordModel newWord);
    public Task<WordModel> UpdateWord(string id, WordModel updWord);
    public Task DeleteWord(string id);
}
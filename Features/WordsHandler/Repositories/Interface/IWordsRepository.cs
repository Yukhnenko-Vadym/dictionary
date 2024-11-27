public interface IWordsRepository<WordModel> where WordModel: class
{
    public Task<WordModel> GetById(string id);
    public Task<IList<WordModel>> GetByTerm(string term);
    public Task<WordModel> CreateWord(WordModel newWord);
    public Task<WordModel> UpdateWord(string id, WordModel updWord);
    public Task DeleteWord(string id);
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WordController: ControllerBase
{
    private readonly IWordService<Word> _wordService;

    public WordController(IWordService<Word> wordService)
    {
        _wordService = wordService;
    }

    [HttpGet("{id}")]
    public async Task<Word> GetWordById(string id)
    {
        return await _wordService.GetWordById(id);
    }

    [HttpGet("term/{term}")]
    public async Task<IList<Word>> GetWordByTerm(string term)
    {
        return await _wordService.GetWordByTerm(term);
    }

    [HttpPost]
    public async Task<Word> CreateWord(Word newWord)
    {
        return await _wordService.CreateWord(newWord);
    }

    [HttpPut("{id}")]
    public async Task<Word> UpdateWord(string id, Word updWord)
    {
        return await _wordService.UpdateWord(id, updWord);
    }

    [HttpDelete("{id}")]
    public async Task DeleteWord(string id)
    {
        await _wordService.DeleteWord(id);
    }  

    [HttpGet("test")]
    [Authorize(Roles = "Default, Admin")]
    public string GetAuthTest()
    {
        return "It works";
    }
}
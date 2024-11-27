using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class WordRequest
{
    public string Term { get; set; }
    public List<WordDefinition> Definition { get; set; }

    public WordRequest(string term, List<WordDefinition> definition)
    {
        Term = term;
        Definition = definition;
    }
}

public class WordRequestDefinition
{
    public string PartOfSpeech { get; set; }
    public string Meaning { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class WordResponse
{
    public string Term { get; set; }
    public List<WordDefinition> Definition { get; set; }

    public WordResponse(string term, List<WordDefinition> definition)
    {
        Term = term;
        Definition = definition;
    }
}

public class WordResponseDefinition
{
    public string PartOfSpeech { get; set; }
    public string Meaning { get; set; }
}
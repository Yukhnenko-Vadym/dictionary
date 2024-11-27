using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Word
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("word")]
    public string Term { get; set; }

    [BsonElement("definition")]
    public List<WordDefinition> Definition { get; set; }

    public Word(string term, List<WordDefinition> definition)
    {
        Term = term;
        Definition = definition;
    }
}

public class WordDefinition
{
    [BsonElement("pos")]
    public string PartOfSpeech { get; set; }

    [BsonElement("meaning")]
    public string Meaning { get; set; }
}
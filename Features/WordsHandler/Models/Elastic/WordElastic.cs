using Microsoft.AspNetCore.Builder.Extensions;

public class WordElastic
{
    public string Id { get; set; }

    public string Term { get; set; }

    public List<WordDefinition> Definition { get; set; }

    public WordElastic(string id, string term, List<WordDefinition> definition)
    {
        Id = id;
        Term = term;
        Definition = definition;
    }
}

// public class WordElasticDefinition
// {
//     public string PartOfSpeech { get; set; }

//     public string Meaning { get; set; }
// }
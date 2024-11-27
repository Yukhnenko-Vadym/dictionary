
using DictionaryApp.Features.UsersAuth.Models.Domain;
using MongoDB.Driver;

namespace DictionaryApp.Data;

public class DbContext 
{
    private readonly  IMongoDatabase _database;

    public DbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Word> Words => _database.GetCollection<Word>("word");
    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
}
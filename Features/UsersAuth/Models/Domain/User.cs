using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DictionaryApp.Features.UsersAuth.Models.Domain;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public string HashedPassword { get; private set; }

     public User(
        string firstName,
        string lastName,
        string email,
        string hashedPassword)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = UserRole.Default;
        HashedPassword = hashedPassword;
    }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void UpdatePassword(string hashedPassword)
    {
        HashedPassword = hashedPassword;
    }
}

namespace DictionaryApp.Features.UsersAuth.Models.Requests;
public class UpdateUserBody
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}
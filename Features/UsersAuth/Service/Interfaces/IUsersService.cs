using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Models.Requests;

namespace DictionaryApp.Features.UsersAuth.Service.Interfaces;

public interface IUsersService
{
    Task<User> GetById(string id);
    Task<User> Update(string id, UpdateUserBody userBody);
    Task UpdatePassword(string id, string password);
    Task Delete(string id);
}
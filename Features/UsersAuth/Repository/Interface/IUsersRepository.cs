using DictionaryApp.Features.UsersAuth.Models.Domain;

namespace DictionaryApp.Features.UsersAuth.Repository.Interface;

public interface IUserRepository
{
    Task<User?> GetById(string id);
    Task<User> Create(User createUser);
    Task<User> Update(string id, User updUser);
    Task Delete(string id);
    Task<bool> IsExist(string email);
    Task<User?> GetByEmail(string email);
}
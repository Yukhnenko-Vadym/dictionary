using DictionaryApp.Data;
using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Repository.Interface;
using MongoDB.Driver;

namespace DictionaryApp.Features.UsersAuth.Repository.Implementations;

public class UsersRepository: IUserRepository
{

    private readonly DbContext _dbContext;

    public UsersRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.Find(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> IsExist(string email)
    {
        return await _dbContext.Users.Find(user => user.Email == email).AnyAsync();
    }

    public async Task<User> Create(User createUser)
    {
        await _dbContext.Users.InsertOneAsync(createUser);        
        return createUser;
    }

    public async Task<User> Update(string id, User updUser)
    {
        await _dbContext.Users.ReplaceOneAsync(updUser =>
            updUser.Id == id,
            updUser
        );
        
        return updUser;
    }

    public async Task Delete(string id)
    {
        await _dbContext.Users.DeleteOneAsync(dltUser => dltUser.Id == id);
    }

    public async Task<User?> GetById(string id)
    {
        return await _dbContext.Users.Find(usr => usr.Id == id).FirstOrDefaultAsync();
    }
}
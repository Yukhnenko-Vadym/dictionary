using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Models.Requests;
using DictionaryApp.Features.UsersAuth.Service.Interfaces;
using DictionaryApp.Features.UsersAuth.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using DictionaryApp.Exceptions;

namespace DictionaryApp.Features.UsersAuth.Service.Implementations;

public class UsersService : IUsersService
{
    private readonly IUserRepository _usersRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    //private readonly IHeadersService _headersService;

    public UsersService(/*IHeadersService headerService,*/ IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _usersRepository = userRepository;
        _passwordHasher = passwordHasher;
        //_headersService = headerService;
    }

    public async Task<User> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Null or empty id of user");
        }

        return await _usersRepository.GetById(id) ?? throw new ArgumentException($"User with id {id} not found");;
    }

    public async Task Delete(string id)
    {
        var user = await _usersRepository.GetById(id);

        if (user == null)
        {
            throw new ArgumentException("User with this id not found");
        }

        //CheckPermissions(user);
        
        await _usersRepository.Delete(id);
    }
    

    public async Task<User> Update(string id, UpdateUserBody userBody)
    {
        var user = await GetById(id);

        if (user.Email != userBody.Email && await _usersRepository.IsExist(userBody.Email))
        {
            throw new ArgumentException("User with this email already exist");
        }

        //CheckPermissions(user);

        user.Update(
            userBody.FirstName, 
            userBody.LastName,
            userBody.Email
        );

        return await _usersRepository.Update(id, user);;
    }

    public async Task UpdatePassword(string id, string password)
    {
        var user = await GetById(id);

        //CheckPermissions(user);

        user.UpdatePassword(_passwordHasher.HashPassword(user, password));

        await _usersRepository.Update(id, user);
    }

    // private void CheckPermissions(User user)
    // {
    //     if (_headersService.UserId.ToString() == user.Id || _headersService.UserRole == UserRole.Admin)
    //     {
    //         return;
    //     }

    //     throw new AuthorizationException("You do not have permissions to update/delete this user");
    // }
}
using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Models.Requests;
using DictionaryApp.Features.UsersAuth.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("user/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUsersService _usersService;

    public UserController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    [Route("getById/")]
    public async Task<User> GetById(string id)
    {
        return await _usersService.GetById(id);
    }

    [HttpPut]
    [Route("updateUser/")]
    public async Task<User> Update(string id, UpdateUserBody userBody)
    {
        return await _usersService.Update(id, userBody);
    }

    [HttpPut]
    [Route("updateUserPwd/")]
    public async Task UpdatePassword(string id, string password)
    {
        await _usersService.UpdatePassword(id, password);
    }

    [HttpDelete]
    [Route("deleteUser/")]
    public async Task Delete(string id)
    {
        await _usersService.Delete(id);
    }
}
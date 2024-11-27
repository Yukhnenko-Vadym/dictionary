using Microsoft.AspNetCore.Mvc;

using DictionaryApp.Features.UsersAuth.Models.Requests;
using DictionaryApp.Features.UsersAuth.Service.Interfaces;

namespace DictionaryApp.Features.UsersAuth.Controllers;

[ApiController]
[Route("auth/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login/")]
    public async Task<string> Login(LoginBody loginBody)
    {
        return await _authService.Login(loginBody);
    }

    [HttpPost]
    [Route("register/")]
    public async Task<string> Register(RegisterBody registerBody)
    {
        return await _authService.Register(registerBody);
    }   
}
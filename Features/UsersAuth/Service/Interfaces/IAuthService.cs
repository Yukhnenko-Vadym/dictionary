using DictionaryApp.Features.UsersAuth.Models.Requests;

namespace DictionaryApp.Features.UsersAuth.Service.Interfaces;
public interface IAuthService
{
    Task<string> Login(LoginBody loginBody);
    Task<string> Register(RegisterBody registerBody);
}
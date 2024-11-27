using System.IdentityModel.Tokens.Jwt;
using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Service.Interfaces;

namespace DictionaryApp.Features.UsersAuth.Service.Implementations;

public class HeadersService //: IHeadersService
{
    // public int? UserId { get; }
    // public UserRole? UserRole { get; }

    // public HeadersService(IHttpContextAccessor httpContextAccessor)
    // {
    //     var headers = httpContextAccessor.HttpContext?.Request.Headers;

    //     if (headers == null || !headers.TryGetValue("Authorization", out var authToken))
    //     {
    //         return;
    //     }

    //     var tokenHandler = new JwtSecurityTokenHandler().ReadJwtToken(authToken.ToString().Replace("Bearer ", ""));

    //     UserId = int.Parse(tokenHandler.Claims.Single(c => c.Type == "id").Value);

    //     UserRole = (UserRole)Enum.Parse(typeof(UserRole), tokenHandler.Claims.Single(c => c.Type == "role").Value);
    // }
}
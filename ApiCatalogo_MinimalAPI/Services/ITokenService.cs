using ApiCatalogo_MinimalAPI.Models;

namespace ApiCatalogo_MinimalAPI.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);
    }
}

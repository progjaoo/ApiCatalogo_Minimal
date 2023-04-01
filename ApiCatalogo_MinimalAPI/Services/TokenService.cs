using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiCatalogo_MinimalAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiCatalogo_MinimalAPI.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(string key, string issuer, string audience, UserModel user)
        {
            //declarações sobre o user
            //compoe o PAYLOAD do token

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName), //nome do usuario
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };
            //gerar uma chave, usando a chave secreta com Symetric...
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //aplicando Algoritmo na chave
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //definir a geração do TOKEN 
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             claims: claims,
                                             expires: DateTime.Now.AddMinutes(10),
                                             signingCredentials: credentials);

            //com token obtido, deserializa o token e retorn string pro usuario
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}

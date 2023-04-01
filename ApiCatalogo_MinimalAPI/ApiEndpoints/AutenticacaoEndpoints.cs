using System.Runtime.CompilerServices;
using ApiCatalogo_MinimalAPI.Models;
using ApiCatalogo_MinimalAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiCatalogo_MinimalAPI.ApiEndpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndpoints(this WebApplication app)
        {
            //ENDPOINT PARA LOGIN 
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                //se o user for nulo, retorna invalido
                if (userModel == null)
                    return Results.BadRequest("Login Inválido");

                //se o usuario for autenticado, chama o servico "GERAR TOKEN"
                if (userModel.UserName == "joaomarcos" && userModel.Password == "numsey#123")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);
                    //se for válido user recebe o token
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inválido");
                }

            }).Produces(StatusCodes.Status400BadRequest).
            Produces(StatusCodes.Status200OK)
            .WithName("Login")
            .WithTags("Autenticacao");
        }
    }
}

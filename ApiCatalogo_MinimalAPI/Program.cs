using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using ApiCatalogo.AppServicesExtensions;
using ApiCatalogo_MinimalAPI.ApiEndpoints;
using ApiCatalogo_MinimalAPI.Context;
using ApiCatalogo_MinimalAPI.Models;
using ApiCatalogo_MinimalAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.AddAutenticationJwt();
builder.Services.AddCors();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var enviroment = app.Environment;
app.UseExceptionHandling(enviroment)
   .UseSwaggerMiddleware()
   .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
using ApiCatalogo_MinimalAPI.Context;
using ApiCatalogo_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo_MinimalAPI.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {
            //mapPOST
            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });

            //MapGET retornar todas categorias
            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categorias").RequireAuthorization();

            //MapGet retornar Categoria por ID
            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id) is Categoria categoria
                            ? Results.Ok(categoria)
                            : Results.NotFound();
            });

            //MapPUT para atualizar uma categoria
            app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                //Verifica se a categoria que ta passando com o ID é diferente
                //do que ta informando
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                //se for igual, prossegue e localiza com FIND ASYNC
                var categoriaDB = await db.Categorias.FindAsync(id);

                //CategoriaDB é os dados que vao alterar
                //verifica se o categoriaDB é null, se for retorna notfoud
                if (categoriaDB is null) return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(categoriaDB);
            });

            //MapDELETE para excluir a categoria
            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                {
                    return Results.NotFound();
                }

                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

        }
    }
}

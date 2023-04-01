using ApiCatalogo_MinimalAPI.Context;
using ApiCatalogo_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo_MinimalAPI.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {
            //ENDPOINTS PARA PRODUTO
            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            });

            //retornando lista de products
            app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

            //retornando Lista pelo ID
            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) => {

                // RETORNO AGUARDA - db.produtos Encontrar asincronamente pelo ID - Is > é produto ? se for OK se não, resultado é not found
                return await db.Produtos.FindAsync(id) is Produto produto ? Results.Ok(produto) : Results.NotFound();

            });

            //atualizar produto
            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {

                if (produto.ProdutoId != id)
                {
                    //se produto, for diferente do produtoID 
                    return Results.BadRequest();
                }

                //se for igual, procura findasync.
                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null)
                    return Results.NotFound();

                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                produtoDB.Imagem = produto.Imagem;
                produtoDB.DataCompra = produto.DataCompra;
                produtoDB.Estoque = produto.Estoque;
                produtoDB.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();

                return Results.Ok(produtoDB);
            });

            //deleta produto
            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var produto = await db.Produtos.FindAsync(id);

                if (produto is null)
                    return Results.NotFound();

                db.Produtos.Remove(produto);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}

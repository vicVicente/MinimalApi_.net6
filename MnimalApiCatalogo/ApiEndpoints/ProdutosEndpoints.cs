using Microsoft.EntityFrameworkCore;
using MnimalApiCatalogo.Context;
using MnimalApiCatalogo.Models;

namespace MnimalApiCatalogo.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {
            app.MapPost("/produto", async (Produto produto, AppDbContext db)
    =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{produto.ProdutoId}", produto);
            });

            app.MapGet("/produto", async (AppDbContext db) =>
                await db.Produtos.AsNoTracking().ToListAsync()).WithTags("Produtos").RequireAuthorization();

            app.MapGet("/produto/{id:int}", async (int id, AppDbContext db)
                =>
            {
                return await db.Produtos.FindAsync(id)
                                is Produto produto
                                ? Results.Ok(produto)
                                : Results.NotFound();
            });

            app.MapPut("/produto/{id:int}", async (int id, Produto produto, AppDbContext db)
                =>
            {
                if (produto.ProdutoId != id)
                    return Results.BadRequest();

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

            app.MapDelete("/produto/{id:int}", async (int id, AppDbContext db)
                =>
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

using EstoqueAPI.Data;
using EstoqueAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProdutoController : Controller
    {
        [HttpGet("produtos")]
        public async Task<IActionResult> Getasync(
            [FromServices] EstoqueDataContext context)
        {
            var produtos = await context.Produtos.ToListAsync();
            return Ok(produtos);
        }

        [HttpGet("produtos/{id:int}")]
        public async Task<IActionResult> GetByIdasync(
            [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            var produto = context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] Produto model)
        {
            await context.Produtos.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/produtos/{model.Id}", model);
        }

        [HttpPut("produtos/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] Produto model,
            [FromRoute] int id)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto == null)
                return NotFound();

            produto.Nome = model.Nome;
            produto.Descricao = model.Descricao;
            produto.Quantidade = model.Quantidade;
            produto.PrecoCompra = model.PrecoCompra;
            produto.PrecoVenda = model.PrecoVenda;
            produto.CompradoEm = model.CompradoEm;

            context.Produtos.Update(produto);
            await context.SaveChangesAsync();

            return Created($"v1/produtos/{model.Id}", model);
        }

        [HttpDelete("produtos/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto == null)
                return NotFound();

            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();

            return Ok(produto);
        }
        
    }
}
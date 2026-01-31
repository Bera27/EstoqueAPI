using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProdutoController : Controller
    {
        [HttpGet("produtos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EstoqueDataContext context)
        {
            var produtos = await context.Produtos.ToListAsync();
            return Ok(produtos);
        }

        [HttpGet("produtos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] EditorProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Quantidade = model.Quantidade,
                    PrecoCompra = model.PrecoCompra,
                    PrecoVenda = model.PrecoVenda,
                    CompradoEm = model.CompradoEm,

                };

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();

                return Created($"v1/produtos/{produto.Id}", produto);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Erro: PCP10 - Não foi possível incluir a categoria");
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro: PCP11 - Falha interna no servidor");
            }
        }

        [HttpPut("produtos/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] EditorProdutoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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

            return Ok(model);
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
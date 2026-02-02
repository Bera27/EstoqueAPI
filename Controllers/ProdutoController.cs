using EstoqueAPI.Data;
using EstoqueAPI.Extension;
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
            try
            {
                var produtos = await context.Produtos
                                .AsNoTracking()
                                .ToListAsync();

                return Ok(new ResultViewModel<List<Produto>>(produtos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>("PCG10 - Falha interna no servidor"));
            }
        }

        [HttpGet("produtos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var produto = await context.Produtos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("PCG21 - Falha interna no servidor"));
            }
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] EditorProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErros()));

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

                return Created($"v1/produtos/{produto.Id}", new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCP30 - Não foi possível incluir o produto"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCP31 - Falha interna no servidor"));
            }
        }

        [HttpPut("produtos/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] EstoqueDataContext context,
            [FromBody] EditorProdutoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErros()));

            try
            {
                var produto = await context.Produtos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Quantidade = model.Quantidade;
                produto.PrecoCompra = model.PrecoCompra;
                produto.PrecoVenda = model.PrecoVenda;
                produto.CompradoEm = model.CompradoEm;

                context.Produtos.Update(produto);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCD50 - Não foi possível atualizar o produto"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCP40 - Falha interna no servidor"));
            }
        }

        [HttpDelete("produtos/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var produto = await context.Produtos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCD50 - Não foi possível excluir o produto"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: PCD51 - Falha interna no servidor"));
            }
        }
        
    }
}
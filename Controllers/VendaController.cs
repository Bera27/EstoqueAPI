using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using EstoqueAPI.ViewModels.Vendas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class VendaController : ControllerBase
    {
        [HttpGet("vendas")]
        public async Task<IActionResult> GetAsync
        ([FromServices] EstoqueDataContext context)
        {
            try
            {
                var vendas = await context.Vendas
                                .AsNoTracking()
                                .Include(x => x.Funcionario)
                                .Select(x => new GetVendaViewModel
                                {
                                    Id = x.Id,
                                    Funcionario = x.Funcionario.Nome,
                                    VendaTotal = x.VendaTotal,
                                    DataVenda = x.DataVenda
                                })
                                .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(vendas));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Venda>>("VC10 - Falha interna no servidor"));
            }
        }

        [HttpGet("vendas/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var venda = await context.Vendas
                                .AsNoTracking()
                                .Include(x => x.Funcionario)
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (venda == null)
                    return NotFound(new ResultViewModel<Venda>(venda));

                return Ok(new ResultViewModel<Venda>(venda));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Venda>>("VC20 - Falha interna no servidor"));
            }
        }

        [HttpPost("vendas")]
        public async Task<IActionResult> PostAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] PostVendaViewModel model)
        {
            if (!ModelState.IsValid)
                BadRequest(new ResultViewModel<Venda>(ModelState.GetErros()));

            try
            {
                var venda = new Venda
                {
                    IdFuncionario = model.IdFuncionario,
                    VendaTotal = model.VendaTotal
                };

                await context.Vendas.AddAsync(venda);
                await context.SaveChangesAsync();

                return Created($"v1/vendas{venda.Id}", new ResultViewModel<Venda>(venda));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VC30 - Não foi possível incluir a venda"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("VC31 - Falha interna no servidor"));
            }
        }

        [HttpPut("vendas/{id:int}")]
        public async Task<IActionResult> PutAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] PostVendaViewModel model,
         [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                BadRequest(new ResultViewModel<Venda>(ModelState.GetErros()));

            try
            {
                var venda = await context.Vendas
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (venda == null)
                    return NotFound(new ResultViewModel<Venda>("venda não encontrada"));

                venda.IdFuncionario = model.IdFuncionario;
                venda.VendaTotal = model.VendaTotal;

                context.Vendas.Update(venda);
                await context.SaveChangesAsync();

                return Created($"v1/vendas{venda.Id}", new ResultViewModel<Venda>(venda));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VC40 - Não foi possível atualizar a venda"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("VC41 - Falha interna no servidor"));
            }
        }

        [HttpDelete("vendas/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var venda = await context.Vendas
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (venda == null)
                    return NotFound(new ResultViewModel<Venda>("venda não encontrada"));

                context.Vendas.Remove(venda);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Venda>(venda));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VC50 - Não foi possível excluir a venda"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("VC51 - Falha interna no servidor"));
            }
        }
    }
}
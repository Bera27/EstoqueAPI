using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using EstoqueAPI.ViewModels.VendaItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    [Authorize(Roles = "admin, atendente")]
    public class VendaItemController : ControllerBase
    {
        [HttpGet("vendas-item")]
        public async Task<IActionResult> GetAsync
        ([FromServices] EstoqueDataContext context)
        {
            try
            {
                var vendaItem = await context.VendaItems
                                    .AsNoTracking()
                                    .Include(x => x.Produto)
                                    .Select(x => new GetVendaItemViewModel
                                    {
                                        Id = x.Id,
                                        Produto = x.Produto.Nome,
                                        Quantidade = x.Quantidade,
                                        PrecoUN = x.PrecoUN
                                    })
                                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(vendaItem));
            }
            catch
            {
                return StatusCode(500, "VIC10 - Falha interna no servidor");
            }
        }

        [HttpGet("vendas-item{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var vendaItem = context.VendaItems
                                    .AsNoTracking()
                                    .Include(x => x.Produto)
                                    .FirstOrDefault(x => x.Id == id);

                if (vendaItem == null)
                    return NotFound("Itens da venda não encontrado");

                return Ok(new ResultViewModel<dynamic>(vendaItem));
            }
            catch
            {
                return StatusCode(500, "VIC20 - Falha interna no servidor");
            }
        }

        [HttpPut("vendas-item/{id:int}")]
        public async Task<IActionResult> PutAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] PostVendaItemViewModel model,
         [FromRoute] int id)
        {
            try
            {
                var vendaItem = await context.VendaItems
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (vendaItem == null)
                    return NotFound("Itens da venda não encontrado");

                vendaItem.IdVenda = model.IdVenda;
                vendaItem.IdProduto = model.IdProduto;
                vendaItem.PrecoUN = model.PrecoUN;
                vendaItem.Quantidade = model.Quantidade;

                context.VendaItems.Update(vendaItem);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<VendaItem>(vendaItem));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VIC40 - Não foi possível atualizar os itens da venda"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VIC41 - Falha interna no servidor"));
            }
        }

        [HttpDelete("vendas-item/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var vendaItem = await context.VendaItems
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (vendaItem == null)
                    return NotFound("Itens da venda não encontrado");

                context.VendaItems.Remove(vendaItem);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<VendaItem>(vendaItem));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VIC50 - Não foi possível excluir os itens da venda"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VIC51 - Falha interna no servidor"));
            }
        }
    }
}
using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using EstoqueAPI.ViewModels.Vendas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    [Authorize(Roles = "admin, atendente")]
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

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var venda = new Venda
                {
                    IdFuncionario = model.IdFuncionario,
                    VendaTotal = 0, // Vai ser calculado abaixo
                    Itens = new List<VendaItem>()
                };

                decimal totalVenda = 0;

                foreach (var itemInput in model.Itens)
                {
                    var produto = await context.Produtos.FindAsync(itemInput.IdProduto);
                    if (produto == null)
                        return BadRequest(new ResultViewModel<string>($"Produto ID {itemInput.IdProduto} não encontrado."));

                    if (produto.Quantidade < itemInput.Quantidade)
                        return BadRequest(new ResultViewModel<string>($"Estoque insuficiente para o produto: {produto.Nome}."));

                    var vendaItem = new VendaItem
                    {
                        Produto = produto,
                        Quantidade = itemInput.Quantidade,
                        PrecoUN = produto.PrecoVenda
                    };

                    produto.Quantidade -= itemInput.Quantidade;
                    totalVenda += (produto.PrecoVenda * itemInput.Quantidade);

                    venda.Itens.Add(vendaItem);

                    context.Produtos.Update(produto);
                }

                venda.VendaTotal = totalVenda;
                await context.Vendas.AddAsync(venda);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Created($"v1/vendas{venda.Id}", new ResultViewModel<Venda>(venda));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Desfaz tudo se der erro
                return StatusCode(500, new ResultViewModel<string>("Erro ao processar a venda: " + ex.Message));
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
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (venda == null)
                    return NotFound(new ResultViewModel<Venda>("venda não encontrada"));

                venda.IdFuncionario = model.IdFuncionario;

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
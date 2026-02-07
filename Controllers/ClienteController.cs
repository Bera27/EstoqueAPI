using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    [Authorize(Roles = "admin, atendente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet("clientes")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EstoqueDataContext context)
        {
            try
            {
                var clientes = await context.Clientes
                                    .AsNoTracking()
                                    .ToListAsync();

                return Ok(new ResultViewModel<List<Cliente>>(clientes));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>("CC10 - Falha interna no servidor"));
            }
        }

        [HttpGet("clientes/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (   [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var cliente = await context.Clientes
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>("Cliente não encontrado"));

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>("CC11 - Falha interna no servidor"));
            }
        }

        [HttpPost("clientes")]
        public async Task<IActionResult> PostAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] EditorClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErros()));

            try
            {
                var cliente = new Cliente
                {
                    Nome = model.Nome,
                    Telefone = model.Telefone,
                    Cpf = model.Cpf
                };

                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();

                return Created($"v1/clientes/{cliente.Id}", new ResultViewModel<Cliente>(cliente));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Cliente>("Erro: CC20 - Não foi possível incluir o cliente"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>("CC21 - Falha interna no servidor"));
            }
        }

        [HttpPut("clientes/{id:int}")]
        public async Task<IActionResult> PutAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] EditorClienteViewModel model,
         [FromRoute] int id)
        {
            try
            {
                var cliente = context.Clientes
                                    .FirstOrDefault(x => x.Id == id);

                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>("Cliente não encontrado"));

                cliente.Nome = model.Nome;
                cliente.Cpf = model.Cpf;
                cliente.Telefone = model.Telefone;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>("CC30 - Falha interna no servidor"));
            }
        }

        [HttpDelete("clientes/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var cliente = context.Clientes
                                    .FirstOrDefault(x => x.Id == id);

                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>("Cliente não encontrado"));

                context.Remove(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>("CC40 - Falha interna no servidor"));
            }
        }
    }
}
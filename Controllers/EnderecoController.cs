using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using EstoqueAPI.ViewModels.Enderecos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class EnderecoController : ControllerBase
    {
        [HttpGet("enderecos")]
        public async Task<IActionResult> GetAsync
        ([FromServices] EstoqueDataContext context)
        {
            try
            {
                var enderecos = await context.Enderecos
                                        .AsNoTracking()
                                        .Include(x => x.Cliente)
                                        .Select(x => new GetEnderecoViewModel
                                        {
                                            Id = x.Id,
                                            Rua = x.Rua,
                                            Bairro = x.Bairro,
                                            Numero = x.Numero,
                                            Complemento = x.Complemento,
                                            Cliente = x.Cliente.Nome
                                        })
                                        .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(enderecos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Endereco>>("EDC10 - Falha interna no servidor"));
            }
        }

        [HttpGet("enderecos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var endereco = await context.Enderecos
                                        .AsNoTracking()
                                        .Include(x => x.Cliente)
                                        .Select(x => new GetEnderecoViewModel
                                        {
                                            Id = x.Id,
                                            Rua = x.Rua,
                                            Bairro = x.Bairro,
                                            Numero = x.Numero,
                                            Complemento = x.Complemento,
                                            Cliente = x.Cliente.Nome
                                        })
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if(endereco == null)
                    return BadRequest(new ResultViewModel<Endereco>("Endereço não encontrado"));

                return Ok(new ResultViewModel<dynamic>(endereco));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Endereco>>("EDC20 - Falha interna no servidor"));
            }
        }

        [HttpPost("enderecos")]
        public async Task<IActionResult> PostAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] PostEnderecoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Endereco>(ModelState.GetErros()));
            try
            {
                var endereco = new Endereco
                {
                    Rua = model.Rua,
                    Bairro = model.Bairro,
                    Numero = model.Numero,
                    Complemento = model.Complemento,
                    IdCliente = model.IdCliente
                };

                await context.Enderecos.AddAsync(endereco);
                await context.SaveChangesAsync();

                return Created($"v1/enderecos/{endereco.Id}", new ResultViewModel<Endereco>(endereco));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: EDC30 - Não foi possível incluir o endereço"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("EDC31 - Falha interna no servidor"));
            }
        }

        [HttpPut("enderecos/{id:int}")]
        public async Task<IActionResult> PutAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] PostEnderecoViewModel model,
         [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Endereco>(ModelState.GetErros()));
            try
            {
                var endereco = context.Enderecos
                                    .AsNoTracking()
                                    .FirstOrDefault(x => x.Id == id);

                if (endereco == null)
                    return NotFound(new ResultViewModel<Endereco>("Endereço não encontrado"));

                endereco.Rua = model.Rua;
                endereco.Bairro = model.Bairro;
                endereco.Numero = model.Numero;
                endereco.Complemento = model.Complemento;
                endereco.IdCliente = endereco.IdCliente;

                context.Enderecos.Update(endereco);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Endereco>(endereco));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: EDC40 - Não foi possível atualizar o endereço"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("EDC41 - Falha interna no servidor"));
            }
        }

        [HttpDelete("enderecos/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var endereco = context.Enderecos
                                    .AsNoTracking()
                                    .FirstOrDefault(x => x.Id == id);

                if (endereco == null)
                    return NotFound(new ResultViewModel<Endereco>("Endereço não encontrado"));

                context.Enderecos.Remove(endereco);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Endereco>(endereco));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: EDC50 - Não foi possível excluir o endereço"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Endereco>("EDC51 - Falha interna no servidor"));
            }
        }
    }
}
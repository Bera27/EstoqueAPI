using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Migrations;
using EstoqueAPI.Models;
using EstoqueAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet("funcionarios")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EstoqueDataContext context)
        {
            try
            {
                var funcionario = await context.Funcionarios
                                    .AsNoTracking()
                                    .Select(x => new
                                    {
                                        x.Id,
                                        x.Nome,
                                        x.Telefone,
                                        x.Cargo
                                    })
                                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(funcionario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("FC10 - Falha interna no servidor"));
            }
        }

        [HttpGet("funcionarios/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByIdAsync
        (   [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var funcionario = await context.Funcionarios
                                        .AsNoTracking()
                                        .Select(x => new
                                        {
                                            x.Id,
                                            x.Nome,
                                            x.Telefone,
                                            x.Cargo
                                        })
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if (funcionario == null)
                    return NotFound(new ResultViewModel<Funcionario>("Funcionario não encontrado"));

                return Ok(new ResultViewModel<dynamic>(funcionario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("FC11 - Falha interna no servidor"));
            }
        }

        [HttpPost("funcionarios")]
        public async Task<IActionResult> PostAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] EditorFuncionarioViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Funcionario>(ModelState.GetErros()));

            try
            {
                var funcionario = new Funcionario
                {
                    Nome = model.Nome,
                    Telefone = model.Telefone,
                    Cargo = model.Cargo,
                    Senha = model.Senha
                };

                funcionario.Senha = PasswordHasher.Hash(funcionario.Senha);

                await context.Funcionarios.AddAsync(funcionario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    funcionario = funcionario.Nome,
                    funcionario.Senha
                }));
            }
            catch (DbUpdateException ex)
            {
                var baseEx = ex.GetBaseException();
                return BadRequest(new { error = baseEx.Message, stack = baseEx.StackTrace });
                //return StatusCode(500, new ResultViewModel<Produto>("Erro: VC30 - Não foi possível incluir o funcionario"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>($"FC21 - Falha interna no servidor"));
            }
        }

        [HttpPut("funcionarios/{id:int}")]
        public async Task<IActionResult> PutAsync
        ([FromServices] EstoqueDataContext context,
         [FromBody] EditorFuncionarioViewModel model,
         [FromRoute] int id)
        {
            try
            {
                var funcionario = context.Funcionarios
                                    .FirstOrDefault(x => x.Id == id);

                if (funcionario == null)
                    return NotFound(new ResultViewModel<Funcionario>("Funcionario não encontrado"));

                funcionario.Nome = model.Nome;
                funcionario.Cargo = model.Cargo;
                funcionario.Telefone = model.Telefone;

                context.Funcionarios.Update(funcionario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Funcionario>(funcionario));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VC30 - Não foi possível atualizar o funcionario"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("FC31 - Falha interna no servidor"));
            }
        }

        [HttpDelete("funcionarios/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var funcionario = context.Funcionarios
                                    .FirstOrDefault(x => x.Id == id);

                if (funcionario == null)
                    return NotFound(new ResultViewModel<Funcionario>("Funcionario não encontrado"));

                context.Remove(funcionario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Funcionario>(funcionario));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro: VC40 - Não foi possível excluir o funcionario"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("FC41 - Falha interna no servidor"));
            }
        }
    }
}
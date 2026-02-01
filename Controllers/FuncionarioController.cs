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
    public class FuncionarioController : ControllerBase
    {
        [HttpGet("funcionarios")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EstoqueDataContext context)
        {
            try
            {
                var funcionario = await context.Funcionarios
                                    .AsNoTracking()
                                    .ToListAsync();

                return Ok(new ResultViewModel<List<Funcionario>>(funcionario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("CC10 - Falha interna no servidor"));
            }
        }

        [HttpGet("funcionarios/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (   [FromServices] EstoqueDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var funcionario = await context.Funcionarios
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if (funcionario == null)
                    return NotFound(new ResultViewModel<Funcionario>("Funcionario não encontrado"));

                return Ok(new ResultViewModel<Funcionario>(funcionario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("CC11 - Falha interna no servidor"));
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
                    Cargo = model.Cargo
                };

                await context.Funcionarios.AddAsync(funcionario);
                await context.SaveChangesAsync();

                return Created($"v1/funcionarios/{funcionario.Id}", new ResultViewModel<Funcionario>(funcionario));
            }
            catch (DbUpdateException ex)
            {
                var baseEx = ex.GetBaseException();
                return BadRequest(new { error = baseEx.Message, stack = baseEx.StackTrace });
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("CC21 - Falha interna no servidor"));
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
                                    .AsNoTracking()
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
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("CC30 - Falha interna no servidor"));
            }
        }

        [HttpDelete("funcionarios/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        ([FromServices] EstoqueDataContext context,
         [FromRoute] int id)
        {
            try
            {
                var funcionario = context.Funcionarios
                                    .AsNoTracking()
                                    .FirstOrDefault(x => x.Id == id);

                if (funcionario == null)
                    return NotFound(new ResultViewModel<Funcionario>("Funcionario não encontrado"));

                context.Remove(funcionario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Funcionario>(funcionario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Funcionario>("CC40 - Falha interna no servidor"));
            }
        }
    }
}
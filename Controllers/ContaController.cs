using EstoqueAPI.Data;
using EstoqueAPI.Extension;
using EstoqueAPI.Models;
using EstoqueAPI.Services;
using EstoqueAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    public class ContaController : ControllerBase
    {

        [HttpPost("v1/contas/login")]
        public async Task<IActionResult> Login(
            [FromServices] TokenService tokenService,
            [FromServices] EstoqueDataContext context,
            [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

            var funcionario = await context.Funcionarios
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Nome == model.Nome);

            if (funcionario == null)
                return StatusCode(401, new ResultViewModel<string>("Nome ou senha inválidos"));

            if (!PasswordHasher.Verify(funcionario.Senha, model.Senha))
                return StatusCode(401, new ResultViewModel<string>("Nome ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(funcionario);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("CCL - Falha interna no servidor"));
            }
        }
    }
}
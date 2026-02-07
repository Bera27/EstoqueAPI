using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstoqueAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace EstoqueAPI.Services
{
    public class TokenService
    {
        public string GenerateToken(Funcionario funcionario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, funcionario.Nome),
                    new(ClaimTypes.Role, funcionario.Cargo),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
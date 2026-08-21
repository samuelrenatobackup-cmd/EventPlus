
using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public LoginController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            var usuarioEncontrado =
                await _usuario.BuscarPorEmailESenha(
                    DTO.Email,
                    DTO.Senha
                );

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Email ou senha inválidos!");
            }

            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    usuarioEncontrado.IdUsuario.ToString()
                ),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    usuarioEncontrado.Email
                ),

                new Claim(
                    "nome",
                    usuarioEncontrado.Nome
                ),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                )
            };

            var chaveSecreta = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    "eventos-chaves-autenticacao-webapi-dev"
                )
            );

            var credenciais = new SigningCredentials(
                chaveSecreta,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: "EventPlusWebAPI",
                audience: "EventPlusWebAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            var tokenString =
                new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Expiracao = token.ValidTo,

                Usuario = new
                {
                    usuarioEncontrado.IdUsuario,
                    usuarioEncontrado.Nome,
                    usuarioEncontrado.Email
                }
            });
        }
    }
}

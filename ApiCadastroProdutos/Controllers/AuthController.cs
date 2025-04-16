using Microsoft.AspNetCore.Mvc;
using ApiCadastroProdutos.Models;
using ApiCadastroProdutos.Services;
using Microsoft.Extensions.Configuration;

namespace ApiCadastroProdutos.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private static List<Usuario> usuarios = new(); // Lista simulando um banco de dados (por enquanto)

        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario); // Salva o usuário na lista
            return Ok("Usuário registrado.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario credenciais)
        {
            // Verifica se o usuário existe com o email e senha fornecidos
            var usuario = usuarios.FirstOrDefault(u =>
                u.Email == credenciais.Email &&
                u.Senha == credenciais.Senha);

            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            // Gera o token e retorna
            var token = TokenService.GerarToken(usuario, _config["JwtSettings:SecretKey"]);
            return Ok(new { token });
        }
    }
}

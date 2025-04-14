using System.IdentityModel.Tokens.Jwt;      //Para gerar e manipular o token
using System.Security.Claims;               //Para armazenar as "informações do usuário" no token
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ApiCadastroProdutos.Models;

nanmespace ApiCadastroProdutos.Services
{
    public static class TokenService
    {
        public static string GerarToken(Usuario usuario, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);  //Chave secreta para gerar o token

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),   //add o nome do usuário ao token
                    new Claim(ClaimTypes.Email, usuario.Email)  //add o email 
                }),
                Expires = DateTime.UtcNow.addHours(2),          //Validade do token: 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // retorna o token gerado
        }
    }
}
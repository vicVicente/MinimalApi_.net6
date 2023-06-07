using Microsoft.IdentityModel.Tokens;
using MnimalApiCatalogo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MnimalApiCatalogo.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(string key, string issuer, string audience, UserModel user)
        {
            var claims = new[] //Composição do payload do token
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            // Gerando uma chave criptografada, com base na chave secreta
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //Aplicando um algoritmo na cahve secreta sendo ele o "HmacSha256", obtendo uma chave simétrica
            var credentials = new SigningCredentials(securityKey,
                                                            SecurityAlgorithms.HmacSha256);
            //Geração do token, usando os dados definidos 
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler(); 
            var stringToken = tokenHandler.WriteToken(token);
            
            return stringToken;
        }
    }
}

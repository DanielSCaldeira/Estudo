using Microsoft.IdentityModel.Tokens; // Tipos para manipula��o de tokens e chaves de seguran�a
using System.IdentityModel.Tokens.Jwt; // Tipos para cria��o e manipula��o de JWT
using System.Security.Claims; // Tipos para trabalhar com claims (informa��es do usu�rio)
using System.Text; // Utilit�rios para manipula��o de texto e codifica��o

namespace Estudo.Infrastructure
{
    // Classe utilit�ria para gera��o de tokens JWT
    public static class JwtTokenGenerator
    {
        // M�todo est�tico para gerar um token JWT
        public static string GerarToken(int id, string usuario, string email, string role, string issuer, string audience, string secretKey)
        {
            // Cria um array de claims (informa��es do usu�rio e permiss�es)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()), // Adiciona o ID do usu�rio como claim
                new Claim(ClaimTypes.Name, usuario),                // Adiciona o nome do usu�rio como claim
                new Claim(ClaimTypes.Email, email),                 // Adiciona o email do usu�rio como claim
                new Claim(ClaimTypes.Role, role)                   // Adiciona o papel (role) do usu�rio como claim
            };

            // Cria a chave de seguran�a a partir da string secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // Cria as credenciais de assinatura usando a chave e o algoritmo HMAC SHA256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria o token JWT com as informa��es fornecidas
            var token = new JwtSecurityToken(
                issuer: issuer,           // Quem emite o token
                audience: audience,       // Quem pode consumir o token
                claims: claims,           // Informa��es do usu�rio
                expires: DateTime.Now.AddDays(1), // Tempo de expira��o do token (1 dia)
                signingCredentials: creds // Credenciais de assinatura
            );

            // Serializa o token para string e retorna
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
/*
JwtTokenGenerator.cs: Classe est�tica que centraliza a gera��o de tokens JWT.
Permite criar tokens de autentica��o de forma padronizada e reutiliz�vel.
Cada linha est� comentada para facilitar o entendimento do fluxo de gera��o do token.
*/

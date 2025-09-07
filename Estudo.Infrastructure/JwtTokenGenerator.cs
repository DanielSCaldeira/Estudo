using Microsoft.IdentityModel.Tokens; // Tipos para manipulação de tokens e chaves de segurança
using System.IdentityModel.Tokens.Jwt; // Tipos para criação e manipulação de JWT
using System.Security.Claims; // Tipos para trabalhar com claims (informações do usuário)
using System.Text; // Utilitários para manipulação de texto e codificação

namespace Estudo.Infrastructure
{
    // Classe utilitária para geração de tokens JWT
    public static class JwtTokenGenerator
    {
        // Método estático para gerar um token JWT
        public static string GerarToken(int id, string usuario, string email, string role, string issuer, string audience, string secretKey)
        {
            // Cria um array de claims (informações do usuário e permissões)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()), // Adiciona o ID do usuário como claim
                new Claim(ClaimTypes.Name, usuario),                // Adiciona o nome do usuário como claim
                new Claim(ClaimTypes.Email, email),                 // Adiciona o email do usuário como claim
                new Claim(ClaimTypes.Role, role)                   // Adiciona o papel (role) do usuário como claim
            };

            // Cria a chave de segurança a partir da string secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // Cria as credenciais de assinatura usando a chave e o algoritmo HMAC SHA256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria o token JWT com as informações fornecidas
            var token = new JwtSecurityToken(
                issuer: issuer,           // Quem emite o token
                audience: audience,       // Quem pode consumir o token
                claims: claims,           // Informações do usuário
                expires: DateTime.Now.AddDays(1), // Tempo de expiração do token (1 dia)
                signingCredentials: creds // Credenciais de assinatura
            );

            // Serializa o token para string e retorna
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
/*
JwtTokenGenerator.cs: Classe estática que centraliza a geração de tokens JWT.
Permite criar tokens de autenticação de forma padronizada e reutilizável.
Cada linha está comentada para facilitar o entendimento do fluxo de geração do token.
*/

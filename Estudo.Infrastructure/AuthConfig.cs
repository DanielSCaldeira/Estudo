using Microsoft.AspNetCore.Authentication.JwtBearer; // Tipos para configurar autenticação JWT no ASP.NET Core
using Microsoft.Extensions.DependencyInjection; // Tipos para registrar serviços na injeção de dependência
using Microsoft.IdentityModel.Tokens; // Tipos para validação de tokens
using System.Text; // Utilitários para manipulação de texto e codificação

namespace Estudo.Infrastructure
{
    // Classe de configuração para autenticação JWT
    public static class AuthConfig
    {
        // Método de extensão para registrar autenticação JWT na aplicação
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string issuer, string audience, string secretKey)
        {
            // Adiciona e configura o serviço de autenticação JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Define o esquema padrão de autenticação
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // Define o esquema padrão de desafio
            })
            .AddJwtBearer(options =>
            {
                // Configura os parâmetros de validação do token JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Valida o emissor do token
                    ValidateAudience = true, // Valida o público do token
                    ValidateLifetime = true, // Valida o tempo de expiração do token
                    ValidateIssuerSigningKey = true, // Valida a chave de assinatura
                    ValidIssuer = issuer, // Define o emissor válido
                    ValidAudience = audience, // Define o público válido
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // Define a chave de assinatura
                };
            });
            return services; // Retorna a coleção de serviços para encadeamento
        }
    }
}
/*
AuthConfig.cs: Classe estática que expõe um método de extensão para registrar a autenticação JWT na aplicação.
Permite centralizar e reutilizar a configuração de autenticação em diferentes projetos.
Cada linha está comentada para facilitar o entendimento do fluxo de configuração.
*/

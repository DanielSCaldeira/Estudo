using Microsoft.AspNetCore.Authentication.JwtBearer; // Tipos para configurar autentica��o JWT no ASP.NET Core
using Microsoft.Extensions.DependencyInjection; // Tipos para registrar servi�os na inje��o de depend�ncia
using Microsoft.IdentityModel.Tokens; // Tipos para valida��o de tokens
using System.Text; // Utilit�rios para manipula��o de texto e codifica��o

namespace Estudo.Infrastructure
{
    // Classe de configura��o para autentica��o JWT
    public static class AuthConfig
    {
        // M�todo de extens�o para registrar autentica��o JWT na aplica��o
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string issuer, string audience, string secretKey)
        {
            // Adiciona e configura o servi�o de autentica��o JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Define o esquema padr�o de autentica��o
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // Define o esquema padr�o de desafio
            })
            .AddJwtBearer(options =>
            {
                // Configura os par�metros de valida��o do token JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Valida o emissor do token
                    ValidateAudience = true, // Valida o p�blico do token
                    ValidateLifetime = true, // Valida o tempo de expira��o do token
                    ValidateIssuerSigningKey = true, // Valida a chave de assinatura
                    ValidIssuer = issuer, // Define o emissor v�lido
                    ValidAudience = audience, // Define o p�blico v�lido
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // Define a chave de assinatura
                };
            });
            return services; // Retorna a cole��o de servi�os para encadeamento
        }
    }
}
/*
AuthConfig.cs: Classe est�tica que exp�e um m�todo de extens�o para registrar a autentica��o JWT na aplica��o.
Permite centralizar e reutilizar a configura��o de autentica��o em diferentes projetos.
Cada linha est� comentada para facilitar o entendimento do fluxo de configura��o.
*/

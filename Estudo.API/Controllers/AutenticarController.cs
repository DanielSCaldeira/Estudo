using Estudo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Estudo.API.Configuration;

public static class AutenticarController
{
    // Agora recebe RouteGroupBuilder para seguir o mesmo padrão das outras controllers
    public static void RegisterEndpoints(RouteGroupBuilder group, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var issuer = jwtSection["Issuer"] ?? "";
        var audience = jwtSection["Audience"] ?? "";
        var secretKey = jwtSection["SecretKey"] ?? "";

        // Endpoint para autenticar e gerar token
        group.MapPost("/login", ([FromBody] LoginRequest login) =>
        {
            if (login.Usuario == "admin" && login.Senha == "123")
            {
                // Exemplo fixo, substitua por busca real do usuário se necessário
                var id = 1;
                var nome = "admin";
                var email = "admin@email.com";
                var role = "Admin";
                var token = JwtTokenGenerator.GerarToken(
                    id: id,
                    usuario: nome,
                    email: email,
                    role: role,
                    issuer: issuer,
                    audience: audience,
                    secretKey: secretKey
                );
                return Results.Ok(new { token });
            }
            return Results.Unauthorized();
        });

        // Endpoint para recuperar informações do token
        group.MapGet("/me", [Authorize] (ClaimsPrincipal user) =>
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nome = user.Identity?.Name;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            return Results.Ok(new { id, nome, email, role });
        });

        // Endpoint para atualizar/renovar o token
        group.MapPost("/refresh", ([FromBody] RefreshRequest refresh) =>
        {
            if (!string.IsNullOrEmpty(refresh.Token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(refresh.Token);
                var id = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
                var nome = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "";
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";
                var novoToken = JwtTokenGenerator.GerarToken(
                    id: int.TryParse(id, out var idInt) ? idInt : 0,
                    usuario: nome,
                    email: email,
                    role: role,
                    issuer: issuer,
                    audience: audience,
                    secretKey: secretKey
                );
                return Results.Ok(new { token = novoToken });
            }
            return Results.BadRequest();
        });
    }
}

public record LoginRequest(string Usuario, string Senha);
public record RefreshRequest(string Token);

/*
Agora AutenticarController segue o mesmo padrão das controllers Cliente e Pedido,
recebendo RouteGroupBuilder e IConfiguration para registro dos endpoints.
*/

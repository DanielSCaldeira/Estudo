// Este arquivo � o ponto de entrada da aplica��o ASP.NET Core usando Minimal API.
// Abaixo, cada linha � explicada para facilitar o entendimento da arquitetura:
using Estudo.Infrastructure; // Importa AuthConfig para configura��o centralizada de autentica��o JWT
using Estudo.API.Configuration; // Importa a extens�o

var builder = WebApplication.CreateBuilder(args); // Inicializa o builder para configurar servi�os e a aplica��o

// Recupera configura��es JWT do appsettings.json
var jwtSection = builder.Configuration.GetSection("Jwt"); // Obt�m a se��o Jwt do arquivo de configura��o
var issuer = jwtSection["Issuer"] ?? ""; // Recupera o emissor do token
var audience = jwtSection["Audience"] ?? ""; // Recupera o p�blico do token
var secretKey = jwtSection["SecretKey"] ?? ""; // Recupera a chave secreta do token

// Configura��o do JWT centralizada
builder.Services.AddJwtAuthentication(
    issuer: issuer, // Usa o emissor recuperado do appsettings.json
    audience: audience, // Usa o p�blico recuperado do appsettings.json
    secretKey: secretKey // Usa a chave secreta recuperada do appsettings.json
);

// Adiciona servi�os ao container de depend�ncias. Aqui, o Swagger � configurado para documenta��o da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Informe o token Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthorization();

var app = builder.Build(); // Constr�i a aplica��o com as configura��es definidas

// Configura o pipeline de requisi��es HTTP. Se estiver em ambiente de desenvolvimento, ativa o Swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o documento Swagger
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Mostra menu lateral, um grupo expandido por vez
        // options.DefaultModelsExpandDepth(-1); // Descomente para ocultar modelos de schemas
    });
}

app.UseHttpsRedirection(); // Redireciona todas as requisi��es HTTP para HTTPS
app.UseAuthentication(); // Adiciona o middleware de autentica��o JWT
app.UseAuthorization(); // Adiciona o middleware de autoriza��o

// Registra endpoints p�blicos e protegidos de forma centralizada
EndpointAuthorizationExtensions.RegisterProtectedEndpoints(app);

app.Run(); // Inicia a aplica��o e come�a a escutar requisi��es

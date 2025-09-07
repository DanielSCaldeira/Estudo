// Este arquivo é o ponto de entrada da aplicação ASP.NET Core usando Minimal API.
// Abaixo, cada linha é explicada para facilitar o entendimento da arquitetura:
using Estudo.Infrastructure; // Importa AuthConfig para configuração centralizada de autenticação JWT
using Estudo.API.Configuration; // Importa a extensão

var builder = WebApplication.CreateBuilder(args); // Inicializa o builder para configurar serviços e a aplicação

// Recupera configurações JWT do appsettings.json
var jwtSection = builder.Configuration.GetSection("Jwt"); // Obtém a seção Jwt do arquivo de configuração
var issuer = jwtSection["Issuer"] ?? ""; // Recupera o emissor do token
var audience = jwtSection["Audience"] ?? ""; // Recupera o público do token
var secretKey = jwtSection["SecretKey"] ?? ""; // Recupera a chave secreta do token

// Configuração do JWT centralizada
builder.Services.AddJwtAuthentication(
    issuer: issuer, // Usa o emissor recuperado do appsettings.json
    audience: audience, // Usa o público recuperado do appsettings.json
    secretKey: secretKey // Usa a chave secreta recuperada do appsettings.json
);

// Adiciona serviços ao container de dependências. Aqui, o Swagger é configurado para documentação da API.
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

var app = builder.Build(); // Constrói a aplicação com as configurações definidas

// Configura o pipeline de requisições HTTP. Se estiver em ambiente de desenvolvimento, ativa o Swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o documento Swagger
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Mostra menu lateral, um grupo expandido por vez
        // options.DefaultModelsExpandDepth(-1); // Descomente para ocultar modelos de schemas
    });
}

app.UseHttpsRedirection(); // Redireciona todas as requisições HTTP para HTTPS
app.UseAuthentication(); // Adiciona o middleware de autenticação JWT
app.UseAuthorization(); // Adiciona o middleware de autorização

// Registra endpoints públicos e protegidos de forma centralizada
EndpointAuthorizationExtensions.RegisterProtectedEndpoints(app);

app.Run(); // Inicia a aplicação e começa a escutar requisições

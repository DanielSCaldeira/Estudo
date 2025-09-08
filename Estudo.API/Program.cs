// Este arquivo � o ponto de entrada da aplica��o ASP.NET Core usando Minimal API.
// Abaixo, cada linha � explicada para facilitar o entendimento da arquitetura:
using Estudo.Infrastructure; // Importa AuthConfig para configura��o centralizada de autentica��o JWT
using Estudo.API.Configuration;
using Estudo.Service; // Importa a extens�o
using AspNetCoreRateLimit; // Rate Limiting para limitar requisi��es
using Prometheus; // Para expor m�tricas para Prometheus

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

// Registre os servi�os para GraphQL (necess�rio para resolver queries/mutations)
builder.Services.AddSingleton<ClienteService>();
builder.Services.AddSingleton<PedidoService>();

// Configura o servidor GraphQL e registra os tipos de Query e Mutation
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Estudo.API.GraphQL.Query>()
    .AddMutationType<Estudo.API.GraphQL.Mutation>();

// Adiciona configura��o de Rate Limiting (limita requisi��es por IP)
builder.Configuration.AddJsonFile("appsettings.ratelimit.json", optional: true, reloadOnChange: true);
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

// Configura CORS (libera apenas m�todos seguros e origens confi�veis)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:5001", "https://localhost:7001") // ajuste conforme necess�rio
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Prote��o extra contra XSS/CSRF via headers e antiforgery
builder.Services.AddAntiforgery(); // CSRF para endpoints que usam cookies

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

// Removido redirecionamento HTTPS para padronizar comportamento HTTP no Docker
// app.UseHttpsRedirection(); // Redireciona todas as requisi��es HTTP para HTTPS
app.UseAuthentication(); // Adiciona o middleware de autentica��o JWT
app.UseAuthorization(); // Adiciona o middleware de autoriza��o
app.UseIpRateLimiting(); // Rate Limiting: limita requisi��es por IP
app.UseCors("DefaultCorsPolicy"); // CORS: controla origens permitidas

// Prote��o extra: headers de seguran�a contra XSS, clickjacking e vazamento de informa��es
app.Use(async (context, next) =>
{
    // Impede que o navegador tente adivinhar o tipo de conte�do
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    // Ativa prote��o contra XSS nos navegadores
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    // Garante que o navegador n�o envie o cabe�alho Referer
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    // Impede que o site seja carregado em iframes (protege contra clickjacking)
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    await next();
});

// Endpoint GraphQL para queries e mutations
app.MapGraphQL("/graphql");

// Endpoint para m�tricas Prometheus
app.UseMetricServer(); // Cria o endpoint /metrics para Prometheus
app.UseHttpMetrics();  // Coleta m�tricas de requisi��es HTTP

// Registra endpoints p�blicos e protegidos de forma centralizada
EndpointAuthorizationExtensions.RegisterProtectedEndpoints(app);

app.Run(); // Inicia a aplica��o e come�a a escutar requisi��es

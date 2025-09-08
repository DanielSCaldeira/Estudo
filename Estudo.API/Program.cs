// Este arquivo é o ponto de entrada da aplicação ASP.NET Core usando Minimal API.
// Abaixo, cada linha é explicada para facilitar o entendimento da arquitetura:
using Estudo.Infrastructure; // Importa AuthConfig para configuração centralizada de autenticação JWT
using Estudo.API.Configuration;
using Estudo.Service; // Importa a extensão
using AspNetCoreRateLimit; // Rate Limiting para limitar requisições
using Prometheus; // Para expor métricas para Prometheus

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

// Registre os serviços para GraphQL (necessário para resolver queries/mutations)
builder.Services.AddSingleton<ClienteService>();
builder.Services.AddSingleton<PedidoService>();

// Configura o servidor GraphQL e registra os tipos de Query e Mutation
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Estudo.API.GraphQL.Query>()
    .AddMutationType<Estudo.API.GraphQL.Mutation>();

// Adiciona configuração de Rate Limiting (limita requisições por IP)
builder.Configuration.AddJsonFile("appsettings.ratelimit.json", optional: true, reloadOnChange: true);
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

// Configura CORS (libera apenas métodos seguros e origens confiáveis)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:5001", "https://localhost:7001") // ajuste conforme necessário
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Proteção extra contra XSS/CSRF via headers e antiforgery
builder.Services.AddAntiforgery(); // CSRF para endpoints que usam cookies

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

// Removido redirecionamento HTTPS para padronizar comportamento HTTP no Docker
// app.UseHttpsRedirection(); // Redireciona todas as requisições HTTP para HTTPS
app.UseAuthentication(); // Adiciona o middleware de autenticação JWT
app.UseAuthorization(); // Adiciona o middleware de autorização
app.UseIpRateLimiting(); // Rate Limiting: limita requisições por IP
app.UseCors("DefaultCorsPolicy"); // CORS: controla origens permitidas

// Proteção extra: headers de segurança contra XSS, clickjacking e vazamento de informações
app.Use(async (context, next) =>
{
    // Impede que o navegador tente adivinhar o tipo de conteúdo
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    // Ativa proteção contra XSS nos navegadores
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    // Garante que o navegador não envie o cabeçalho Referer
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    // Impede que o site seja carregado em iframes (protege contra clickjacking)
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    await next();
});

// Endpoint GraphQL para queries e mutations
app.MapGraphQL("/graphql");

// Endpoint para métricas Prometheus
app.UseMetricServer(); // Cria o endpoint /metrics para Prometheus
app.UseHttpMetrics();  // Coleta métricas de requisições HTTP

// Registra endpoints públicos e protegidos de forma centralizada
EndpointAuthorizationExtensions.RegisterProtectedEndpoints(app);

app.Run(); // Inicia a aplicação e começa a escutar requisições

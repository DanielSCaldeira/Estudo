# Estudo - Projeto .NET 9

## Visão Geral
Este projeto é composto por múltiplos módulos organizados em diferentes projetos, utilizando .NET 9 e C# 13.0. O objetivo é demonstrar uma arquitetura moderna para APIs REST, autenticação JWT e testes automatizados.

## Estrutura dos Projetos
- **Estudo.API**: API principal, implementa endpoints REST usando Minimal APIs, autenticação/autorização, e documentação Swagger/OpenAPI.
- **Estudo.Model**: Contém as entidades de domínio (ex: Cliente, Pedido).
- **Estudo.Service**: Implementa regras de negócio e serviços relacionados às entidades.
- **Estudo.Infrastructure**: Gerencia infraestrutura, como autenticação JWT e geração de tokens.
- **Estudo.Teste**: Testes automatizados usando xUnit.

## Principais Ferramentas e Tecnologias
- **.NET 9**: Plataforma principal de desenvolvimento.
- **C# 13.0**: Linguagem utilizada.
- **Minimal APIs**: Estrutura simplificada para criação de endpoints HTTP.
- **Autenticação JWT**: Proteção de endpoints usando tokens JWT.
- **Swagger/OpenAPI**: Documentação automática dos endpoints via Swashbuckle.
- **xUnit**: Framework de testes unitários.

## Dependências Instaladas

### Estudo.API
- `Microsoft.AspNetCore.OpenApi` (9.0.8): Suporte à documentação OpenAPI.
- `Swashbuckle.AspNetCore` (9.0.4): Geração de documentação Swagger.

### Estudo.Infrastructure
- `Microsoft.AspNetCore.Authentication.JwtBearer` (9.0.8): Autenticação JWT para APIs protegidas.

### Estudo.Teste
- `coverlet.collector` (6.0.2): Coleta de cobertura de testes.
- `Microsoft.NET.Test.Sdk` (17.12.0): Suporte à execução de testes.
- `xunit` (2.9.2): Framework de testes unitários.
- `xunit.runner.visualstudio` (2.8.2): Runner para Visual Studio.

## Como Executar
1. Instale o .NET 9 SDK.
2. Restaure os pacotes: `dotnet restore`
3. Execute a API: `dotnet run --project Estudo.API`
4. Acesse a documentação Swagger em `/swagger`.
5. Execute os testes: `dotnet test Estudo.Teste`

## Observações
- Os endpoints de `/clientes` e `/pedidos` são protegidos por autenticação JWT.
- O endpoint `/auth` permite autenticação e geração de tokens.
- O projeto segue boas práticas de separação de responsabilidades.

---
# Estudo - Projeto .NET 9

## Vis�o Geral
Este projeto � composto por m�ltiplos m�dulos organizados em diferentes projetos, utilizando .NET 9 e C# 13.0. O objetivo � demonstrar uma arquitetura moderna para APIs REST, autentica��o JWT e testes automatizados.

## Estrutura dos Projetos
- **Estudo.API**: API principal, implementa endpoints REST usando Minimal APIs, autentica��o/autoriza��o, e documenta��o Swagger/OpenAPI.
- **Estudo.Model**: Cont�m as entidades de dom�nio (ex: Cliente, Pedido).
- **Estudo.Service**: Implementa regras de neg�cio e servi�os relacionados �s entidades.
- **Estudo.Infrastructure**: Gerencia infraestrutura, como autentica��o JWT e gera��o de tokens.
- **Estudo.Teste**: Testes automatizados usando xUnit.

## Principais Ferramentas e Tecnologias
- **.NET 9**: Plataforma principal de desenvolvimento.
- **C# 13.0**: Linguagem utilizada.
- **Minimal APIs**: Estrutura simplificada para cria��o de endpoints HTTP.
- **Autentica��o JWT**: Prote��o de endpoints usando tokens JWT.
- **Swagger/OpenAPI**: Documenta��o autom�tica dos endpoints via Swashbuckle.
- **xUnit**: Framework de testes unit�rios.

## Depend�ncias Instaladas

### Estudo.API
- `Microsoft.AspNetCore.OpenApi` (9.0.8): Suporte � documenta��o OpenAPI.
- `Swashbuckle.AspNetCore` (9.0.4): Gera��o de documenta��o Swagger.

### Estudo.Infrastructure
- `Microsoft.AspNetCore.Authentication.JwtBearer` (9.0.8): Autentica��o JWT para APIs protegidas.

### Estudo.Teste
- `coverlet.collector` (6.0.2): Coleta de cobertura de testes.
- `Microsoft.NET.Test.Sdk` (17.12.0): Suporte � execu��o de testes.
- `xunit` (2.9.2): Framework de testes unit�rios.
- `xunit.runner.visualstudio` (2.8.2): Runner para Visual Studio.

## Como Executar
1. Instale o .NET 9 SDK.
2. Restaure os pacotes: `dotnet restore`
3. Execute a API: `dotnet run --project Estudo.API`
4. Acesse a documenta��o Swagger em `/swagger`.
5. Execute os testes: `dotnet test Estudo.Teste`

## Observa��es
- Os endpoints de `/clientes` e `/pedidos` s�o protegidos por autentica��o JWT.
- O endpoint `/auth` permite autentica��o e gera��o de tokens.
- O projeto segue boas pr�ticas de separa��o de responsabilidades.

---
# Estudo - Projeto .NET 9

## Vis�o Geral
Este projeto � composto por m�ltiplos m�dulos organizados em diferentes projetos, utilizando .NET 9 e C# 13.0. O objetivo � demonstrar uma arquitetura moderna para APIs REST, autentica��o JWT, GraphQL, mensageria RabbitMQ, rate limiting, m�tricas Prometheus e testes automatizados.

## Estrutura dos Projetos
- **Estudo.API**: API principal, implementa endpoints REST usando Minimal APIs, autentica��o/autoriza��o, documenta��o Swagger/OpenAPI, GraphQL, rate limiting e m�tricas Prometheus.
- **Estudo.Model**: Cont�m as entidades de dom�nio (ex: Cliente, Pedido).
- **Estudo.Service**: Implementa regras de neg�cio e servi�os relacionados �s entidades.
- **Estudo.Infrastructure**: Gerencia infraestrutura, como autentica��o JWT e gera��o de tokens.
- **Estudo.DTO**: Data Transfer Objects e valida��es (FluentValidation).
- **Estudo.RabbitMQ**: Integra��o com mensageria RabbitMQ.
- **Estudo.Teste**: Testes automatizados usando xUnit.

## Principais Ferramentas e Tecnologias
- **.NET 9**: Plataforma principal de desenvolvimento.
- **C# 13.0**: Linguagem utilizada.
- **Minimal APIs**: Estrutura simplificada para cria��o de endpoints HTTP.
- **Autentica��o JWT**: Prote��o de endpoints usando tokens JWT.
- **Swagger/OpenAPI**: Documenta��o autom�tica dos endpoints via Swashbuckle.
- **GraphQL (HotChocolate)**: Queries e mutations flex�veis.
- **RabbitMQ**: Mensageria para comunica��o ass�ncrona.
- **FluentValidation**: Valida��o de DTOs.
- **Rate Limiting (AspNetCoreRateLimit)**: Limita��o de requisi��es por IP.
- **Prometheus**: Exposi��o de m�tricas para monitoramento.
- **xUnit**: Framework de testes unit�rios.
- **Entity Framework Core** (opcional): ORM para persist�ncia de dados.

## Padr�o de Arquitetura
O projeto segue o padr�o de arquitetura em camadas (Layered Architecture), separando API, dom�nio, servi�os, infraestrutura, DTOs, mensageria e testes. Utiliza princ�pios SOLID e boas pr�ticas de separa��o de responsabilidades.

## Comandos para Instala��o das Bibliotecas

```powershell
# ASP.NET Core (Web API)
dotnet add Estudo.API package Microsoft.AspNetCore.App # Framework principal para APIs REST
```

```powershell
# GraphQL para .NET
dotnet add Estudo.API package HotChocolate.AspNetCore # Suporte a GraphQL no ASP.NET Core
```

```powershell
# RabbitMQ Client
dotnet add Estudo.RabbitMQ package RabbitMQ.Client # Cliente para integra��o com RabbitMQ
```

```powershell
# FluentValidation
dotnet add Estudo.DTO package FluentValidation # Biblioteca para valida��o de objetos e DTOs
```

```powershell
# JWT Bearer Authentication
dotnet add Estudo.Infrastructure package Microsoft.AspNetCore.Authentication.JwtBearer # Autentica��o via JWT
```

```powershell
# Rate Limiting
dotnet add Estudo.API package AspNetCoreRateLimit # Limita��o de requisi��es na API
```

```powershell
# Prometheus para m�tricas
dotnet add Estudo.API package prometheus-net.AspNetCore # Exposi��o de m�tricas para Prometheus
```

```powershell
# Swagger/OpenAPI
dotnet add Estudo.API package Swashbuckle.AspNetCore # Documenta��o autom�tica dos endpoints
```

```powershell
# (Opcional) Entity Framework Core (se utilizado para persist�ncia)
dotnet add Estudo.Model package Microsoft.EntityFrameworkCore # ORM para acesso a banco de dados
```

## Como Executar
1. Instale o .NET 9 SDK.
2. Restaure os pacotes: `dotnet restore`
3. Execute a API: `dotnet run --project Estudo.API`
4. Acesse a documenta��o Swagger em `/swagger`.
5. Execute os testes: `dotnet test Estudo.Teste`

## Observa��es
- Os endpoints de `/clientes` e `/pedidos` s�o protegidos por autentica��o JWT.
- O endpoint `/auth` permite autentica��o e gera��o de tokens.
- Endpoints GraphQL dispon�veis em `/graphql`.
- M�tricas Prometheus dispon�veis em `/metrics`.
- Rate limiting configurado por IP.
- O projeto segue boas pr�ticas de separa��o de responsabilidades.

## Explica��o dos Principais Pacotes
- **Microsoft.AspNetCore.App**: Base para APIs REST e recursos do ASP.NET Core.
- **HotChocolate.AspNetCore**: Implementa��o de GraphQL para .NET.
- **RabbitMQ.Client**: Integra��o com mensageria RabbitMQ.
- **FluentValidation**: Valida��o de dados de entrada (DTOs).
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autentica��o JWT.
- **AspNetCoreRateLimit**: Limita��o de requisi��es por IP.
- **prometheus-net.AspNetCore**: Exposi��o de m�tricas para Prometheus.
- **Swashbuckle.AspNetCore**: Documenta��o Swagger/OpenAPI.
- **Microsoft.EntityFrameworkCore**: ORM para persist�ncia de dados (opcional).
- **xUnit**: Testes automatizados.

---
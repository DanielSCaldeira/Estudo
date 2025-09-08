# Estudo - Projeto .NET 9

## Visão Geral
Este projeto é composto por múltiplos módulos organizados em diferentes projetos, utilizando .NET 9 e C# 13.0. O objetivo é demonstrar uma arquitetura moderna para APIs REST, autenticação JWT, GraphQL, mensageria RabbitMQ, rate limiting, métricas Prometheus e testes automatizados.

## Estrutura dos Projetos
- **Estudo.API**: API principal, implementa endpoints REST usando Minimal APIs, autenticação/autorização, documentação Swagger/OpenAPI, GraphQL, rate limiting e métricas Prometheus.
- **Estudo.Model**: Contém as entidades de domínio (ex: Cliente, Pedido).
- **Estudo.Service**: Implementa regras de negócio e serviços relacionados às entidades.
- **Estudo.Infrastructure**: Gerencia infraestrutura, como autenticação JWT e geração de tokens.
- **Estudo.DTO**: Data Transfer Objects e validações (FluentValidation).
- **Estudo.RabbitMQ**: Integração com mensageria RabbitMQ.
- **Estudo.Teste**: Testes automatizados usando xUnit.

## Principais Ferramentas e Tecnologias
- **.NET 9**: Plataforma principal de desenvolvimento.
- **C# 13.0**: Linguagem utilizada.
- **Minimal APIs**: Estrutura simplificada para criação de endpoints HTTP.
- **Autenticação JWT**: Proteção de endpoints usando tokens JWT.
- **Swagger/OpenAPI**: Documentação automática dos endpoints via Swashbuckle.
- **GraphQL (HotChocolate)**: Queries e mutations flexíveis.
- **RabbitMQ**: Mensageria para comunicação assíncrona.
- **FluentValidation**: Validação de DTOs.
- **Rate Limiting (AspNetCoreRateLimit)**: Limitação de requisições por IP.
- **Prometheus**: Exposição de métricas para monitoramento.
- **xUnit**: Framework de testes unitários.
- **Entity Framework Core** (opcional): ORM para persistência de dados.

## Padrão de Arquitetura
O projeto segue o padrão de arquitetura em camadas (Layered Architecture), separando API, domínio, serviços, infraestrutura, DTOs, mensageria e testes. Utiliza princípios SOLID e boas práticas de separação de responsabilidades.

## Comandos para Instalação das Bibliotecas

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
dotnet add Estudo.RabbitMQ package RabbitMQ.Client # Cliente para integração com RabbitMQ
```

```powershell
# FluentValidation
dotnet add Estudo.DTO package FluentValidation # Biblioteca para validação de objetos e DTOs
```

```powershell
# JWT Bearer Authentication
dotnet add Estudo.Infrastructure package Microsoft.AspNetCore.Authentication.JwtBearer # Autenticação via JWT
```

```powershell
# Rate Limiting
dotnet add Estudo.API package AspNetCoreRateLimit # Limitação de requisições na API
```

```powershell
# Prometheus para métricas
dotnet add Estudo.API package prometheus-net.AspNetCore # Exposição de métricas para Prometheus
```

```powershell
# Swagger/OpenAPI
dotnet add Estudo.API package Swashbuckle.AspNetCore # Documentação automática dos endpoints
```

```powershell
# (Opcional) Entity Framework Core (se utilizado para persistência)
dotnet add Estudo.Model package Microsoft.EntityFrameworkCore # ORM para acesso a banco de dados
```

## Como Executar
1. Instale o .NET 9 SDK.
2. Restaure os pacotes: `dotnet restore`
3. Execute a API: `dotnet run --project Estudo.API`
4. Acesse a documentação Swagger em `/swagger`.
5. Execute os testes: `dotnet test Estudo.Teste`

## Observações
- Os endpoints de `/clientes` e `/pedidos` são protegidos por autenticação JWT.
- O endpoint `/auth` permite autenticação e geração de tokens.
- Endpoints GraphQL disponíveis em `/graphql`.
- Métricas Prometheus disponíveis em `/metrics`.
- Rate limiting configurado por IP.
- O projeto segue boas práticas de separação de responsabilidades.

## Explicação dos Principais Pacotes
- **Microsoft.AspNetCore.App**: Base para APIs REST e recursos do ASP.NET Core.
- **HotChocolate.AspNetCore**: Implementação de GraphQL para .NET.
- **RabbitMQ.Client**: Integração com mensageria RabbitMQ.
- **FluentValidation**: Validação de dados de entrada (DTOs).
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autenticação JWT.
- **AspNetCoreRateLimit**: Limitação de requisições por IP.
- **prometheus-net.AspNetCore**: Exposição de métricas para Prometheus.
- **Swashbuckle.AspNetCore**: Documentação Swagger/OpenAPI.
- **Microsoft.EntityFrameworkCore**: ORM para persistência de dados (opcional).
- **xUnit**: Testes automatizados.

---
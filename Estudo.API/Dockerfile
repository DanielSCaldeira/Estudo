# Dockerfile para aplicação Estudo.API (.NET 9)
# Imagem base para rodar aplicações ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app # Define o diretório de trabalho dentro do container
EXPOSE 5034  # Expõe a porta 5000 para acesso HTTP
EXPOSE 7020  # Expõe a porta 5001 para acesso HTTPS

# Imagem para build do projeto (SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src # Diretório de trabalho para build
COPY . .     # Copia todos os arquivos do projeto para o container
RUN dotnet restore "Estudo.API/Estudo.API.csproj" # Restaura dependências NuGet
RUN dotnet publish "Estudo.API/Estudo.API.csproj" -c Release -o /app/publish # Compila e publica o projeto

# Imagem final para rodar a aplicação
FROM base AS final
WORKDIR /app # Diretório de trabalho final
COPY --from=build /app/publish . # Copia os arquivos publicados do build
ENTRYPOINT ["dotnet", "Estudo.API.dll"] # Comando de inicialização do container

# Etapa base: Imagem runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa build: SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas os arquivos necessários para o build
COPY ./Eventos/Eventos.sln ./Eventos/
COPY ./Eventos/EventosApi/EventosApi.csproj ./Eventos/EventosApi/
RUN dotnet restore ./Eventos/Eventos.sln

# Copiar o restante do código e compilar
COPY ./Eventos/ ./Eventos/
WORKDIR /src/Eventos/EventosApi
RUN dotnet build -c Release -o /app/build

# Etapa publish: Publicar a aplicação otimizada para produção
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-build --p:UseAppHost=false

# Etapa final: Configurar a imagem para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventosApi.dll"]

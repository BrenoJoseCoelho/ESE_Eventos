# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar a pasta Eventos (ajustado para refletir a nova estrutura do diretório)
COPY . /src

# Restaurar dependências (ajustado para o caminho correto)
RUN dotnet restore "/src/Eventos/EventosApi.csproj"

# Construir a aplicação
WORKDIR "/src/Eventos"
RUN dotnet build "/src/Eventos/EventosApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "/src/Eventos/EventosApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventosApi.dll"]

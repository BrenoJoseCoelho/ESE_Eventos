# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Eventos ./Eventos  # Copia apenas a pasta 'Eventos' (que contém o projeto)
WORKDIR /src/Eventos      # Define o diretório de trabalho como 'Eventos'
RUN dotnet restore "EventosApi.csproj"  # Agora o caminho está correto
RUN dotnet build "EventosApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Eventos      # Certifique-se de estar no diretório correto
RUN dotnet publish "EventosApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventosApi.dll"]

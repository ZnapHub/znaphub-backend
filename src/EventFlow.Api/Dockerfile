FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY EventFlow.sln ./

COPY src/EventFlow.Api ./src/EventFlow.Api
COPY src/EventFlow.Application ./src/EventFlow.Application
COPY src/EventFlow.Domain ./src/EventFlow.Domain
COPY src/EventFlow.Infrastructure ./src/EventFlow.Infrastructure
COPY src/EventFlow.Shared ./src/EventFlow.Shared

RUN dotnet restore EventFlow.sln

WORKDIR /src/src/EventFlow.Api
RUN dotnet build EventFlow.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/src/EventFlow.Api
RUN dotnet publish EventFlow.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

USER root
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
USER appuser

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "EventFlow.Api.dll"]

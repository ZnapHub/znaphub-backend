FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ZnapHub.sln ./

COPY src/ZnapHub.Api ./src/ZnapHub.Api
COPY src/ZnapHub.Application ./src/ZnapHub.Application
COPY src/ZnapHub.Domain ./src/ZnapHub.Domain
COPY src/ZnapHub.Infrastructure ./src/ZnapHub.Infrastructure
COPY src/ZnapHub.Shared ./src/ZnapHub.Shared

RUN dotnet restore ZnapHub.sln

WORKDIR /src/src/ZnapHub.Api
RUN dotnet build ZnapHub.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/src/ZnapHub.Api
RUN dotnet publish ZnapHub.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

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

ENTRYPOINT ["dotnet", "ZnapHub.Api.dll"]

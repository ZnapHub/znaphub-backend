FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Znaphub.sln ./

COPY src/Znaphub.Api ./src/Znaphub.Api
COPY src/Znaphub.Application ./src/Znaphub.Application
COPY src/Znaphub.Domain ./src/Znaphub.Domain
COPY src/Znaphub.Infrastructure ./src/Znaphub.Infrastructure
COPY src/Znaphub.Shared ./src/Znaphub.Shared

RUN dotnet restore Znaphub.sln

WORKDIR /src/src/Znaphub.Api
RUN dotnet build Znaphub.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/src/Znaphub.Api
RUN dotnet publish Znaphub.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

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

ENTRYPOINT ["dotnet", "Znaphub.Api.dll"]

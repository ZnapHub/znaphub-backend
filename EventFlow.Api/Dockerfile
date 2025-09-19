FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EventFlow.sln", "./"]
COPY ["EventFlow.Api/EventFlow.Api.csproj", "EventFlow.Api/"]
COPY ["EventFlow.Application/EventFlow.Application.csproj", "EventFlow.Application/"]
COPY ["EventFlow.Domain/EventFlow.Domain.csproj", "EventFlow.Domain/"]
COPY ["EventFlow.Infrastructure/EventFlow.Infrastructure.csproj", "EventFlow.Infrastructure/"]
COPY ["EventFlow.Shared/EventFlow.Shared.csproj", "EventFlow.Shared/"]

RUN dotnet restore "EventFlow.sln"

COPY . .

WORKDIR "/src/EventFlow.Api"
RUN dotnet build "EventFlow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EventFlow.Api.csproj" -c $BU_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "EventFlow.Api.dll"]
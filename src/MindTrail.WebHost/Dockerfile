FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
ARG version=0.0.0

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG version

WORKDIR /app
COPY src/MindTrail.WebHost/docker-entrypoint.sh docker-entrypoint.sh
RUN apt-get update && \
    apt-get install -y dos2unix && \
    rm -rf /var/lib/apt/lists/*
RUN dos2unix docker-entrypoint.sh && \
    apt-get --purge remove -y dos2unix
RUN chmod +x docker-entrypoint.sh

WORKDIR /source
COPY NuGet.Config .
COPY MindTrail.sln .
COPY src/MindTrail.WebHost/ src/MindTrail.WebHost/
COPY src/MindTrail.HostConfiguration/ src/MindTrail.HostConfiguration/
COPY src/MindTrail.WebApi/ src/MindTrail.WebApi/
COPY src/MindTrail.WebAuth/ src/MindTrail.WebAuth/
COPY src/MindTrail.Common/ src/MindTrail.Common/
RUN dotnet build src/MindTrail.WebHost/MindTrail.WebHost.csproj

FROM build AS publish
ARG version
WORKDIR /source/src/MindTrail.WebHost
RUN dotnet publish MindTrail.WebHost.csproj -p:Version=$version -r linux-x64 -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/docker-entrypoint.sh .
RUN apt-get update && \
    apt-get install -y curl && \
    rm -rf /var/lib/apt/lists/*

EXPOSE 8080
HEALTHCHECK --interval=10s --timeout=10s --retries=3 CMD curl -sS http://localhost/health || exit 1
ENTRYPOINT ["/app/docker-entrypoint.sh"]
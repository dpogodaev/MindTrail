FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
ARG version=0.0.0

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG version

WORKDIR /app
COPY src/ThoughtGuide.WebHost/docker-entrypoint.sh docker-entrypoint.sh
RUN apt-get update && \
    apt-get install -y dos2unix && \
    rm -rf /var/lib/apt/lists/*
RUN dos2unix docker-entrypoint.sh && \
    apt-get --purge remove -y dos2unix
RUN chmod +x docker-entrypoint.sh

WORKDIR /source
COPY NuGet.Config .
COPY ThoughtGuide.sln .
COPY src/ThoughtGuide.WebHost/ src/ThoughtGuide.WebHost/
COPY src/ThoughtGuide.HostConfiguration/ src/ThoughtGuide.HostConfiguration/
COPY src/ThoughtGuide.WebApi/ src/ThoughtGuide.WebApi/
COPY src/ThoughtGuide.WebAuth/ src/ThoughtGuide.WebAuth/
COPY src/ThoughtGuide.Common/ src/ThoughtGuide.Common/
RUN dotnet build src/ThoughtGuide.WebHost/ThoughtGuide.WebHost.csproj

FROM build AS publish
ARG version
WORKDIR /source/src/ThoughtGuide.WebHost
RUN dotnet publish ThoughtGuide.WebHost.csproj -p:Version=$version -r linux-x64 -o /app/publish

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
﻿ARG APP_UID=1000
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Set working directory for build context
WORKDIR /build

# Copy only the project files and restore dependencies
COPY src/TwistedTaleweaver.Bridge/*.csproj src/TwistedTaleweaver.Bridge/
COPY src/TwistedTaleweaver.Kafka.Api/*.csproj src/TwistedTaleweaver.Kafka.Api/
COPY src/TwistedTaleweaver.Core/*.csproj src/TwistedTaleweaver.Core/
COPY src/TwistedTaleweaver.Integration.Twitch/*.csproj src/TwistedTaleweaver.Integration.Twitch/

RUN dotnet restore src/TwistedTaleweaver.Bridge/TwistedTaleweaver.Bridge.csproj

# Copy the rest of the source code
COPY src/TwistedTaleweaver.Bridge/. src/TwistedTaleweaver.Bridge/.
COPY src/TwistedTaleweaver.Kafka.Api/. src/TwistedTaleweaver.Kafka.Api/.
COPY src/TwistedTaleweaver.Core/. src/TwistedTaleweaver.Core/.
COPY src/TwistedTaleweaver.Integration.Twitch/. src/TwistedTaleweaver.Integration.Twitch/.

# Build and publish in one step
RUN dotnet publish "src/TwistedTaleweaver.Bridge/TwistedTaleweaver.Bridge.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TwistedTaleweaver.Bridge.dll"]

ARG APP_UID=1000
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Set working directory for build context
WORKDIR /build

# Copy only the project files and restore dependencies
COPY src/TwistedTaleweaver.Database/*.csproj src/TwistedTaleweaver.Database/

RUN dotnet restore src/TwistedTaleweaver.Database/TwistedTaleweaver.Database.csproj

# Copy the rest of the source code
COPY src/TwistedTaleweaver.Database/. src/TwistedTaleweaver.Database/.

# Build and publish in one step
RUN dotnet publish "src/TwistedTaleweaver.Database/TwistedTaleweaver.Database.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TwistedTaleweaver.Database.dll"]

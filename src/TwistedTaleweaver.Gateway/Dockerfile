FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

# Set working directory for build context
WORKDIR /build

# Copy only the project files and restore dependencies
COPY src/TwistedTaleweaver.Gateway/*.csproj src/TwistedTaleweaver.Gateway/
COPY src/TwistedTaleweaver/*.csproj src/TwistedTaleweaver/

RUN dotnet restore src/TwistedTaleweaver.Gateway/TwistedTaleweaver.Gateway.csproj

# Copy the rest of the source code
COPY src/TwistedTaleweaver.Gateway/. src/TwistedTaleweaver.Gateway/
COPY src/TwistedTaleweaver/. src/TwistedTaleweaver/

# Build and publish in one step
RUN dotnet publish "src/TwistedTaleweaver.Gateway/TwistedTaleweaver.Gateway.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TwistedTaleweaver.Gateway.dll"]

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
COPY src/TwistedTaleweaver.Domain/*.csproj src/TwistedTaleweaver.Domain/
COPY src/TwistedTaleweaver.Integration.Clerk/*.csproj src/TwistedTaleweaver.Integration.Clerk/
COPY src/TwistedTaleweaver.Integration.OpenAi/*.csproj src/TwistedTaleweaver.Integration.OpenAi/
COPY src/TwistedTaleweaver/*.csproj src/TwistedTaleweaver/
COPY src/TwistedTaleweaver.Api/*.csproj src/TwistedTaleweaver.Api/
COPY src/TwistedTaleweaver.Core/*.csproj src/TwistedTaleweaver.Core/
COPY src/TwistedTaleweaver.DataAccess/*.csproj src/TwistedTaleweaver.DataAccess/

RUN dotnet restore src/TwistedTaleweaver.Gateway/TwistedTaleweaver.Gateway.csproj

# Copy the rest of the source code
COPY src/TwistedTaleweaver.Gateway/. src/TwistedTaleweaver.Gateway/
COPY src/TwistedTaleweaver.Domain/. src/TwistedTaleweaver.Domain/
COPY src/TwistedTaleweaver.Integration.Clerk/. src/TwistedTaleweaver.Integration.Clerk/
COPY src/TwistedTaleweaver.Integration.OpenAi/. src/TwistedTaleweaver.Integration.OpenAi/
COPY src/TwistedTaleweaver/. src/TwistedTaleweaver/
COPY src/TwistedTaleweaver.Api/. src/TwistedTaleweaver.Api/
COPY src/TwistedTaleweaver.Core/. src/TwistedTaleweaver.Core/
COPY src/TwistedTaleweaver.DataAccess/. src/TwistedTaleweaver.DataAccess/

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

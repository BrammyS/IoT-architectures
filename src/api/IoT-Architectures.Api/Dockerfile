# Setup base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0.10 AS base

WORKDIR /app
EXPOSE 80

# Setup build image
FROM mcr.microsoft.com/dotnet/sdk:7.0.400 AS build

# Setup workspace
WORKDIR /src
COPY /src .
WORKDIR /src/api/IoT-Architectures.Api

# Restore dependencies
RUN dotnet restore "IoT-Architectures.Api.csproj"

# Publish the application
RUN dotnet publish "IoT-Architectures.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Setup final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "IoT-Architectures.Api.dll"]
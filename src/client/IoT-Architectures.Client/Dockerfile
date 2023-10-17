FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/client/IoT-Architectures.Client/IoT-Architectures.Client.csproj", "src/client/IoT-Architectures.Client/"]
COPY ["src/client/IoT-Architectures.Client.Core/IoT-Architectures.Client.Core.csproj", "src/client/IoT-Architectures.Client.Core/"]
COPY ["src/client/IoT-Architectures.Client.Domain/IoT-Architectures.Client.Domain.csproj", "src/client/IoT-Architectures.Client.Domain/"]
RUN dotnet restore "src/client/IoT-Architectures.Client/IoT-Architectures.Client.csproj"
COPY . .
WORKDIR "/src/src/client/IoT-Architectures.Client"
RUN dotnet build "IoT-Architectures.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoT-Architectures.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoT-Architectures.Client.dll"]

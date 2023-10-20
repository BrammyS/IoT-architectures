#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 7.0 -InstallDir ./dotnet
./dotnet/dotnet --version
./dotnet/dotnet publish "IoT-Architectures.Client/IoT-Architectures.Client.csproj" -c Release -o output
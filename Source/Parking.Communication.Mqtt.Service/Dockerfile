#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Parking.Communication.Mqtt.Service/Parking.Communication.Mqtt.Service.csproj", "Parking.Communication.Mqtt.Service/"]
COPY ["Parking.Communication.Mqtt/Parking.Communication.Mqtt.Library.csproj", "Parking.Communication.Mqtt/"]
RUN dotnet restore "Parking.Communication.Mqtt.Service/Parking.Communication.Mqtt.Service.csproj"
COPY . .
WORKDIR "/src/Parking.Communication.Mqtt.Service"
RUN dotnet build "Parking.Communication.Mqtt.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Parking.Communication.Mqtt.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parking.Communication.Mqtt.Service.dll"]
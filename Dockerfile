﻿# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /service
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./Api/Api.csproj ./Api/Api.csproj
COPY ./Core/Core.csproj ./Core/Core.csproj
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/Infrastructure.csproj
RUN dotnet restore "./Api/Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Api/Api.csproj" -c Release -o /service/build

FROM build AS publish 
RUN dotnet publish "./Api/Api.csproj" -c Release -o /service/publish

FROM base AS final
WORKDIR /service
COPY ./docker/.dev.env .env
COPY --from=publish /service/publish .
ENTRYPOINT ["dotnet", "Api.dll", "--environment=Production"]
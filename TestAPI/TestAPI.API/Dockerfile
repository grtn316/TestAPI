#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443



FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TestAPI/TestAPI.API/TestAPI.API.csproj", "TestAPI/TestAPI.API/"]
COPY ["TestAPI/TestAPI.API.Common/TestAPI.API.Common.csproj", "TestAPI/TestAPI.API.Common/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["TestAPI/TestAPI.API.DataContracts/TestAPI.API.DataContracts.csproj", "TestAPI/TestAPI.API.DataContracts/"]
COPY ["TestAPI/TestAPI.Services/TestAPI.Services.csproj", "TestAPI/TestAPI.Services/"]
COPY ["TestAPI/TestAPI.IoC.Configuration/TestAPI.IoC.Configuration.csproj", "TestAPI/TestAPI.IoC.Configuration/"]
RUN dotnet restore "TestAPI/TestAPI.API/TestAPI.API.csproj"
COPY . .
WORKDIR "/src/TestAPI/TestAPI.API"
RUN dotnet build "TestAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestAPI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestAPI.API.dll"]
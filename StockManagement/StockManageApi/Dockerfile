#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StockManageApi/StockManageApi.csproj", "StockManageApi/"]
COPY ["StockManageService/StockManageService.csproj", "StockManageService/"]
COPY ["StockManageRepository/StockManageRepository.csproj", "StockManageRepository/"]
COPY ["StockManageDAO/StockManageDAO.csproj", "StockManageDAO/"]
COPY ["StockManageBusinessObjects/StockManageBusinessObjects.csproj", "StockManageBusinessObjects/"]
RUN dotnet restore "./StockManageApi/StockManageApi.csproj"
COPY . .
WORKDIR "/src/StockManageApi"
RUN dotnet build "./StockManageApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockManageApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockManageApi.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EStockApp.Stock/EStockApp.Stock.csproj", "EStockApp.Stock/"]
RUN dotnet restore "EStockApp.Stock/EStockApp.Stock.csproj"
COPY . .
WORKDIR "/src/EStockApp.Stock"
RUN dotnet build "EStockApp.Stock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EStockApp.Stock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EStockApp.Stock.dll"]

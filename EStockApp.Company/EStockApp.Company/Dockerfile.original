#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EStockApp.Company/EStockApp.Company.csproj", "EStockApp.Company/"]
RUN dotnet restore "EStockApp.Company/EStockApp.Company.csproj"
COPY . .
WORKDIR "/src/EStockApp.Company"
RUN dotnet build "EStockApp.Company.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EStockApp.Company.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EStockApp.Company.dll"]
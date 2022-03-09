#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalog-api/Broooms.Catalog.Api/Broooms.Catalog.Api.csproj", "catalog-api/Broooms.Catalog.Api/"]
RUN dotnet restore "catalog-api/Broooms.Catalog.Api/Broooms.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/catalog-api/Broooms.Catalog.Api"
RUN dotnet build "Broooms.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Broooms.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Broooms.Catalog.Api.dll"]

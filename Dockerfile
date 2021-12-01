FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY PortfolioApi.sln ./
COPY Api/*.csproj ./Api/
COPY Facades/*.csproj ./Facades/
COPY Models/*.csproj ./Models/
COPY Services/*.csproj ./Services/
COPY Tests/*.csproj ./Tests/

RUN dotnet restore
COPY . .

WORKDIR /src/Api
RUN dotnet build -c Release -o /app

WORKDIR /src/Facades
RUN dotnet build -c Release -o /app

WORKDIR /src/Models
RUN dotnet build -c Release -o /app

WORKDIR /src/Services
RUN dotnet build -c Release -o /app

WORKDIR /src/Tests
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet PortfolioApi.dll

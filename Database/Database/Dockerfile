FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Database/Database/Database.csproj", "Database/"]
RUN dotnet restore "Database/Database.csproj"
COPY . .
WORKDIR "/src/Database"

RUN mkdir -p /app/build && rm -rf /app/build/*

RUN dotnet build "Database.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Database.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Database.dll"]
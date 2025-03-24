FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Database/Database/Database.csproj", "Database/Database/"]
COPY ["Protos", "Protos"]
RUN dotnet restore "Database/Database/Database.csproj"
COPY . .
WORKDIR "/src/Database/Database"
RUN dotnet build "Database.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Database.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Database.dll"]

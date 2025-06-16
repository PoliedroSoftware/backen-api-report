FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Poliedro.Report.Api/Poliedro.Report.Api.csproj", "Poliedro.Report.Api/"]
RUN dotnet restore "./Poliedro.Report.Api/Poliedro.Report.Api.csproj"
COPY . .
WORKDIR "/src/Poliedro.Report.Api"
RUN dotnet build "./Poliedro.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Poliedro.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poliedro.Report.Api.dll"]
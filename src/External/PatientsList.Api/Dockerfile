# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/External/PatientsList.Api/PatientsList.Api.csproj", "src/External/PatientsList.Api/"]
COPY ["src/Core/PatientsList.Application/PatientsList.Application.csproj", "src/Core/PatientsList.Application/"]
COPY ["src/Core/PatientsList.Domain/PatientsList.Domain.csproj", "src/Core/PatientsList.Domain/"]
COPY ["src/External/PatientsList.Infrastructure.Sql/PatientsList.Infrastructure.Sql.csproj", "src/External/PatientsList.Infrastructure.Sql/"]
RUN dotnet restore "./src/External/PatientsList.Api/PatientsList.Api.csproj"
COPY . .
WORKDIR "/src/src/External/PatientsList.Api"
RUN dotnet build "./PatientsList.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientsList.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientsList.Api.dll"]
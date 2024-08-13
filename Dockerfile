#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SecureGate.API/SecureGate.API.csproj", "SecureGate.API/"]
COPY ["SecureGate.Application/SecureGate.Application.csproj", "SecureGate.Application/"]
COPY ["SecureGate.Domain/SecureGate.Domain.csproj", "SecureGate.Domain/"]
COPY ["SecureGate.SharedKernel/SecureGate.SharedKernel.csproj", "SecureGate.SharedKernel/"]
COPY ["SecureGate.Infrastructure/SecureGate.Infrastructure.csproj", "SecureGate.Infrastructure/"]
COPY ["SecureGate.Repository/SecureGate.Repository.csproj", "SecureGate.Repository/"]
RUN dotnet restore "./SecureGate.API/SecureGate.API.csproj"
COPY . .
WORKDIR "/src/SecureGate.API"
RUN dotnet build "./SecureGate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SecureGate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecureGate.API.dll"]
# Base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files for restore
COPY ["RentalSystem.API.csproj", "./"]
COPY ["../RentalSystem.Application/RentalSystem.Application.csproj", "../RentalSystem.Application/"]
COPY ["../RentalSystem.Infrastructure/RentalSystem.Infrastructure.csproj", "../RentalSystem.Infrastructure/"]
COPY ["../RentalSystem.Domain/RentalSystem.Domain.csproj", "../RentalSystem.Domain/"]

# Restore dependencies
RUN dotnet restore "RentalSystem.API.csproj"

# Copy remaining files
COPY . .

# Build
RUN dotnet build "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:${PORT}
ENTRYPOINT ["dotnet", "RentalSystem.API.dll"]

# ========================
# Base image for runtime
# ========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ========================
# Build image
# ========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files for restore (relative to solution root)
COPY ["RentalSystem.API/RentalSystem.API.csproj", "RentalSystem.API/"]
COPY ["RentalSystem.Application/RentalSystem.Application.csproj", "RentalSystem.Application/"]
COPY ["RentalSystem.Domain/RentalSystem.Domain.csproj", "RentalSystem.Domain/"]
COPY ["RentalSystem.Infrastructure/RentalSystem.Infrastructure.csproj", "RentalSystem.Infrastructure/"]

# Restore dependencies for API project
RUN dotnet restore "RentalSystem.API/RentalSystem.API.csproj"

# Copy entire solution (relative to build context, i.e., solution root)
COPY . .

# Set working directory to API project
WORKDIR "/src/RentalSystem.API"

# Build API project
RUN dotnet build "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ========================
# Publish stage
# ========================
FROM build AS publish
RUN dotnet publish "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ========================
# Final stage (runtime)
# ========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entrypoint
ENTRYPOINT ["dotnet", "RentalSystem.API.dll"]


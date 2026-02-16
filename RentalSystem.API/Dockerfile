# -------------------------
# Stage 1: Base runtime image
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# -------------------------
# Stage 2: Build stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["RentalSystem.API/RentalSystem.API.csproj", "RentalSystem.API/"]
COPY ["RentalSystem.Application/RentalSystem.Application.csproj", "RentalSystem.Application/"]
COPY ["RentalSystem.Domain/RentalSystem.Domain.csproj", "RentalSystem.Domain/"]
COPY ["RentalSystem.Infrastructure/RentalSystem.Infrastructure.csproj", "RentalSystem.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "RentalSystem.API/RentalSystem.API.csproj"

# Copy the rest of the solution
COPY . .

# Build the API project
WORKDIR "/src/RentalSystem.API"
RUN dotnet build "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# -------------------------
# Stage 3: Publish stage
# -------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentalSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -------------------------
# Stage 4: Final stage
# -------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalSystem.API.dll"]

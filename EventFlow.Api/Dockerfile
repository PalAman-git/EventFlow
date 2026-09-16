# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy package version management file
COPY ["Directory.Packages.props", "./"]

# Copy API project
COPY ["EventFlow.Api/EventFlow.Api.csproj", "EventFlow.Api/"]

# Copy Infrastructure project because API references it
COPY ["EventFlow.Infrastructure/EventFlow.Infrastructure.csproj", "EventFlow.Infrastructure/"]

# Restore API and its project dependencies
RUN dotnet restore "EventFlow.Api/EventFlow.Api.csproj"

# Copy the source code
COPY EventFlow.Api/ EventFlow.Api/
COPY EventFlow.Infrastructure/ EventFlow.Infrastructure/

# Publish API
RUN dotnet publish "EventFlow.Api/EventFlow.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

RUN apt-get update \
    && apt-get install -y curl \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Copy published API
COPY --from=build /app/publish .

# ASP.NET Core container port
EXPOSE 8080

# Start API
ENTRYPOINT ["dotnet", "EventFlow.Api.dll"]
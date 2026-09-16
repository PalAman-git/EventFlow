# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy package version management file
COPY ["Directory.Packages.props", "./"]

# Copy project files
COPY ["EventFlow.Worker/EventFlow.Worker.csproj", "EventFlow.Worker/"]
COPY ["EventFlow.Infrastructure/EventFlow.Infrastructure.csproj", "EventFlow.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "EventFlow.Worker/EventFlow.Worker.csproj"

# Copy source code
COPY EventFlow.Worker/ EventFlow.Worker/
COPY EventFlow.Infrastructure/ EventFlow.Infrastructure/

# Publish worker
RUN dotnet publish "EventFlow.Worker/EventFlow.Worker.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final

WORKDIR /app

# Copy published worker
COPY --from=build /app/publish .

# Start worker
ENTRYPOINT ["dotnet", "EventFlow.Worker.dll"]
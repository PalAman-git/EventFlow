# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy package version management file
COPY ["Directory.Packages.props", "./"]

# Copy project file and restore dependencies
COPY ["Consumer/Consumer.csproj", "Consumer/"]
RUN dotnet restore "Consumer/Consumer.csproj"

# Copy source code
COPY Consumer/ Consumer/

# Build and publish
RUN dotnet publish "Consumer/Consumer.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# ASP.NET Core listens on port 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Consumer.dll"]
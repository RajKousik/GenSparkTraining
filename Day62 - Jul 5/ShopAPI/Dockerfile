# Use the official .NET SDK image for build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the project
COPY . .
RUN dotnet publish -c Release -o /app

# Use the official .NET runtime image for runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ShopAPI.dll"]
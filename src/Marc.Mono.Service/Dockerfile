# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application source code
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port the app runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "YourApp.dll"]

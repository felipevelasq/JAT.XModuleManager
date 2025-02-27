FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Directory.Packages.props file from the root of the solution
COPY Directory.Packages.props ./
COPY Directory.Build.props ./

COPY ./src/JAT.IdentityService.Api/ ./
# Restore as distinct layers
RUN dotnet restore ./JAT.IdentityService.Api/JAT.IdentityService.Api.csproj
# Build and publish a release
RUN dotnet publish ./JAT.IdentityService.Api/JAT.IdentityService.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
# COPY ./src/JAT.Modules.Api/JAT.Modules.Api/bin/Release/net8.0/ /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "JAT.IdentityService.Api.dll"]
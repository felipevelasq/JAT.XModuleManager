FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
# Restore as distinct layers
RUN dotnet restore ./JAT.Modules.Api/JAT.Modules.Api.csproj
# Build and publish a release
RUN dotnet publish ./JAT.Modules.Api/JAT.Modules.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
# COPY ./src/JAT.Modules.Api/JAT.Modules.Api/bin/Release/net8.0/ /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "JAT.Modules.Api.dll"]
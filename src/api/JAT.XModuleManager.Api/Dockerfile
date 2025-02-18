FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./src/api/JAT.XModuleManager.Api/bin/Release/net8.0/ /app
ENTRYPOINT ["dotnet", "JAT.XModuleManager.Api.dll"]
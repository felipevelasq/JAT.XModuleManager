FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./src/api/JAT.XModuleManager.Api/bin/Release/net8.0/ /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "JAT.XModuleManager.Api.dll"]
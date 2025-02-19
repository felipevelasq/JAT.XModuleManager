FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./src/JAT.Modules.Api/JAT.Modules.Api/bin/Release/net8.0/ /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "JAT.Modules.Api.dll"]
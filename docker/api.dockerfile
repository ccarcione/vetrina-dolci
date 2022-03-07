FROM mcr.microsoft.com/dotnet/sdk:5.0 AS netbuilder
WORKDIR /repo
COPY src/VetrinaDolci.WebAPI .
RUN dotnet publish VetrinaDolci.WebAPI.csproj -o /publish/VetrinaDolci.WebAPI

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS prod
WORKDIR /api
COPY --from=netbuilder /publish/VetrinaDolci.WebAPI .
ENTRYPOINT ["dotnet", "VetrinaDolci.WebAPI.dll"]
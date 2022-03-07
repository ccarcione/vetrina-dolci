FROM mcr.microsoft.com/dotnet/sdk:3.1 AS netbuilder
WORKDIR /repo
COPY src/IdentityServer .
RUN dotnet publish IdentityServer.csproj -o /publish/IdentityServer

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS prod
WORKDIR /identityserver
COPY --from=netbuilder /publish/IdentityServer .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]
dotnet dev-certs https --trust
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\VetrinaDolci.pfx -p password
docker compose --file .\docker\docker-compose.testdev.yml --project-directory . --project-name VetrinaDolci-testdev up --build
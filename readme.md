# Vetrina Dolci (IdentityServer4+ .Net Core Web API 5 + EF Core 5 SQLite + Angular 13)



## Descrizione

Vetrina dolci elettronica di una pasticceria. il progetto offre un servizio di identita' utenti, web api consultabili/riutilizzabili e client web multipiattaforma con funzioni di backoffice.

**DEMO**: http://vetrina-dolci.experimenta.cloud/

## Assunzioni

- le immagini di anteprima provengono dal servizio gratuito **pixabay** (https://pixabay.com/api/docs/). non sempre vengono risolte immagini accurate.
  purtroppo i servizi di ricerca immagini hanno costi poco accessibili.
- i dati utilizzati provengono dal sito https://www.dbricette.it/database.htm
- e' stato utilizzato un db SQLite per pura comodita'. in questo modo non e' necessario avere installato alcun motore di databse



## Struttura repository

- _src/IdentityServer/_ --> servizio di identita' (.net core)
- _src/vetrina-dolci-client/_ --> client (angular 13)
- _src/VetrinaDolci.WebAPI/_ --> web api pasticceria
- _src/VetrinaDolci.Client/_ --> console app con alcune funzioni di test
- _.gitlab-ci.yml_ --> configurazione ci-cd gitlab
- _/_ --> appunti vari



## Screen

![show-vetrina-dolci](show-vetrina-dolci.gif)



## HowTo Use

> **requisiti**:
>
> - .net core sdk 5
> - node
> - npm

- clonare il repository

- eseguire i seguenti comandi per avviare il progetto _Identity_ e **VetrinaDolci.WebAPI**:

  ```powershell
  cd .\src\IdentityServer\
  dotnet restore
  dotnet build
  dotnet watch run
  ```

  il servizio e' raggiungibile all'indirizzo [https://localhost:5001/](https://localhost:5001/)

  credenziali utenti (username, password):

  - Luana@email.com, Luana
  - Maria@email.com, Maria
  - bob, bob
  - alice, alice

- eseguire i seguenti comandi per avviare il progetto **VetrinaDolci.WebAPI**:

  ```powershell
  cd .\src\VetrinaDolci.Client\
  dotnet restore
  dotnet build
  dotnet watch run
  ```

  e' possibile interrogare le API all'indirizzo [https://localhost:6001/](https://localhost:6001/)
  al primo avvio viene eseguita la prima migrazione e il seed dei dati. potrebbe volerci un po di tempo.

- eseguire i seguenti comandi per avviare il progetto **vetrina-dolci-client**:

  ```
  cd .\src\vetrina-dolci-client\
  npm i
  ng build
  ng serve
  ```

  l'applicazione e' raggiungibile all'indirizzo [http://localhost:4200/](http://localhost:4200/)

## VetrinaDolce.Client

il progetto e' utile per testare alcune funzioni essenziali del progetto quali:

- login
- comunicazione con le api protette della web API
- parsing csv data e seeding database

## Test

questo progetto non include test. e' possibile pero' testare le web API tramite **Swagger** e Postman.

e' possibile raggiungere il servizio all'indirizzo [https://localhost:6001/swagger/](https://localhost:6001/swagger/) e utilizzare le credenziali OAuth dove necessario.

![show-swagger](show-swagger.gif)
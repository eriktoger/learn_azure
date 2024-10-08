# Repo for exploring different Azure resources
The goal of this repo is to hold the code for various Azure resources.

## Azure Certs taken
- AZ-900 (April 19, 2024)
- AZ-204 (June 30, 2024)
- AZ-400 (August 5, 2024)

## Azure Certs in progress
- ???

## Azure Certs to come
- ???

## Environment variables:
- Non sensitive Backend variables are added to appsettings.Development.json and appsettings.Development.json.
- Semi sensitive variables are added locally via "dotnet user-secrets".
- And to production it is handle under Environments variable in azure.
    - PRIMARY_CONNECTION_STRING: connection string to azure
    - STATISTIC_ID:  The Id to my statistic object in the database
    - APPLICATIONINSIGHTS_CONNECTION_STRING: connection string to azure application insights
    - FUNCTION_URL: The url to http trigger Azure function
    - FUNCTION_CODE: Secret that lets backend call Azure function
    - STORAGE_CONTAINER: the container where the BLOB_SAS_TOKEN goes to.
    - STORAGE_QUEUE_NAME: the queue where QueueConnectionString goes to.
- Frontend are locally added to .env and for Production they needs to be added to [Settings in github](https://github.com/eriktoger/learn_azure/settings/environments).
    - VITE_BACKEND_URL: The url to the backend
    - VITE_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY : instrumentation key to azure application insights
    - VITE_MSAL_CLIENT_ID: Application (client) ID for the app in App registrations
    - VITE_MSAL_AUTHORITY: Directory (tenant) ID for the app in App registrations

## Secrets:
- Secerets are added to the key vault, but locally via "dotnet user-secrets"
    - BlobSasToken: token to connect to blob storage
    - RedisConnectionString: connection string to redis
    - QueueConnectionString: conection string to queue

## Repository secrets
- These secrets live under  "Secrets and variables"/Actions. And they are added automatically when I connected the my github repo to a Azure service.
    - AZUREAPPSERVICE_CLIENTID_<:id>
    - AZUREAPPSERVICE_SUBSCRIPTIONID_<:id>
    - AZUREAPPSERVICE_TENANTID_<:id>
    - AZURE_STATIC_WEB_APPS_API_TOKEN_<:page-name>

## Build andRun docker locally (and shell into)
 - cd backend/DockerContainer
 - docker build -t simple-node .
 - docker run  -p 80:8080 simple-node
 - docker ps
 - docker exec -it \<name that you got from last command> sh

## Push docker container locally
 - docker login \<azure-registry>.azurecr.io
 - docker tag simple-node \<azure-registry>.azurecr.io/simple-node
 - docker push \<azure-registry>.azurecr.io/simple-node:latest

## Currently:
- [Backend / WebApi](https://etogerbackend.azurewebsites.net): A app service written in .net
- [Backend / Swagger](https://etogerbackend.azurewebsites.net/swagger/index.html): Swagger to the backend. There is also an Api Management in azure.
- The Api Management has been disabled/stopped for pricing reasons.
- [Frontend](https://witty-wave-01133fe0f.5.azurestaticapps.net/): A static web app written in Typescript/React
- Database: A Cosmos DB instance
- Docker: a simple node application in a container app / container registry (has been disabled/stopped for pricing reasons)
- Function: Frontend calls WebApi that calls a http trigger Function
- Storage Account: a container for my cat images
- Key vault: Some secrets live in Azure key vault
- Redis: A Redis cache (has been disabled/stopped for pricing reasons).
- Storage Queue: A simple queue where you can push, read and peek from.

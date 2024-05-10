## Repo for exploring different Azure resources
The goal of this repo is to hold the code for various Azure resources.

# Environment variables:
- Non sensitive Backend variables are added to appsettings.Development.json and appsettings.Development.json.
- Sensitive secrets are added locally via "dotnet user-secrets".
- And to production it is handle under Environments variable in azure.
    - PRIMARY_CONNECTION_STRING : connection string to azure
    - STATISTIC_ID :  The Id to my statistic object in the database
    - APPLICATIONINSIGHTS_CONNECTION_STRING : connection string to azure application insights
- Frontend are locally added to .env and for Production they needs to be added to [Settings in github](https://github.com/eriktoger/learn_azure/settings/environments).
    - VITE_BACKEND_URL: The url to the backend
    - VITE_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY : instrumentation key to azure application insights


# Push docker container (no automated CI/CD)
 - docker login \<azure-registry>.azurecr.io
 - docker tag simple-node \<azure-registry>.azurecr.io/simple-node
 - docker push \<azure-registry>.azurecr.io/simple-node:latest

# Currently:
- [Backend / WebApi](https://etogerbackend.azurewebsites.net): A app service written in .net
- [Frontend](https://witty-wave-01133fe0f.5.azurestaticapps.net/): A static web app written in Typescript/React
- Database: A Cosmos DB instance.
- Docker: a simple node application in a container app / container registry
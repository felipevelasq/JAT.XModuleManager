# Modules Service

## Manage Modules
* Create module
* List modules

## How to run locally
1. In a terminal, navigate to the `JAT.Modules.Api` project folder
1. Initialize `user-secrets`
    ```bash
    dotnet user-secrets init
    ```
1. Add token issuer
    ```bash
    dotnet user-secrets set "Auth:Issuer" "identityService"
    ```
1. Add token audience
    ```bash
    dotnet user-secrets set "Auth:Audience" "identityClient"
    ```
1. Add token secret
    ```bash
    dotnet user-secrets set "Auth:Secret" "REPLACE-WITH-YOUR-OWN-SECRET"
    ```
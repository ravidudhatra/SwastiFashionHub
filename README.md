# Swasti Fashion Hub LLP

## Overview

Project to centralize authentication, manage users and permissions based on Identity Server.
Additional GRPC API's to persist the Users, Roles, etc in a Azure Sql Database.

### Core Identity

* .NET 6, Identity Server Project to issue JWT tokens/claims
* Dependency to MTRepository

Seeds the API, clients and Identity Server specifics on startup. 
Two users Alice and Bob can be provisioned with the /seed command.

### SwastiFashionHubAPI

* .NET 7 GRPC API to manage users
* Dependency to MTRepository

### SwastiFashionHub.Data

* .NET 7 EF Core to store the user/role entities

To add additional fields/change the schema, use entity framework migrations:

Open a powershell in the MTRepository folder and execute:
```
 dotnet ef migrations add [MIGRATIONNAME] -o Data\Migrations\ApplicationContext -- "[CONNECTIONSTRING]"
```

IMPORTANT: The migrations are applied during startup of the identity server project.

### SaaSMTGlobalDefs

* .NET 7 Class to share constants

## TODO:

[x] Unit Tests for GRPC API

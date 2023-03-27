# Swasti Fashion Hub LLP

## Overview
Project to manage stock of fabric, design, challan, party etc.

### SwastiFashionHub

* .NET 7, Manage UI
* Dependency to core

### SwastiFashionHub.WebApi

* .NET 7 API to manage database operation. 
* Dependency to SwastiFashionHub.Data

To add additional fields/change the schema, use entity framework migrations:

Open a powershell in the SwastiFashionHub.Data folder and execute:
```
 dotnet ef migrations add [MIGRATIONNAME] -o Data\Migrations\ApplicationContext -- "[CONNECTIONSTRING]"
 
 Insert/Update database
 dotnet ef database update 

 Revert database 
 dotnet ef database update 0

 Revert Migrations
 dotnet ef migrations remove
```


IMPORTANT: The migrations are applied during startup of the identity server project.

## TODO:

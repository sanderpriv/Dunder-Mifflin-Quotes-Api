# The Office Quotes API

## About
* This application is a .NET 6 API that can return a single or a set of random quotes from the American TV-series "The Office".
* Built as an ASP.NET Core application with an SQLite database via EFCore. 

## Endpoints
All endpoints are GETs.

| Endpoint       | Description                          | Parameters |
|----------------|--------------------------------------|------------|
| /quotes/random | Random quote from The Office         |            |
| /quotes/{size} | Set of random quotes from The Office | size: int  |


## Building and running locally
To run the application locally you need to install and create a local database-file.  
Run this command from the directory of the API: ``dotnet ef database update``  
This will create a ``quotes.db`` file at the root directory of the API. 
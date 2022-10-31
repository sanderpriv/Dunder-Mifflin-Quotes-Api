# The Office Quotes API

## About
* This application is a .NET 6 API that can return a single or a set of random quotes from the American TV-series "The Office".
* Built as an ASP.NET Core application with an SQLite database via EFCore. 

## Endpoints
All endpoints are GETs.

| Endpoint           | Description                          | Parameters |
|--------------------|--------------------------------------|------------|
| quote              | Random quote from The Office         |            |
| quotes?size={size} | Set of random quotes from The Office | size: int  |


## Building and running locally
<pre>dotnet ef database update</pre> 
This will create a ``quotes.db`` file at the root directory of the API.  
If you get an error about not having dotnet-ef, install it:
<pre>dotnet tool install --global dotnet-ef</pre>
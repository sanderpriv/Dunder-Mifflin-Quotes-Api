FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dunder.Mifflin.Api/Dunder.Mifflin.Api.csproj", "Dunder.Mifflin.Api/"]
RUN dotnet restore "Dunder.Mifflin.Api/Dunder.Mifflin.Api.csproj"
COPY . .
WORKDIR "/src/Dunder.Mifflin.Api"
RUN dotnet build "Dunder.Mifflin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dunder.Mifflin.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dunder.Mifflin.Api.dll"]

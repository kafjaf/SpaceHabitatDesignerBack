## Étape 1 : Build
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#WORKDIR /src
#
## Copier tous les fichiers de la solution
#COPY . .
#
## Restaurer les packages NuGet
#RUN dotnet restore "SpaceHouse.API/SpaceHouse.API.csproj"
#
## Publier le projet en Release
#RUN dotnet publish "SpaceHouse.API/SpaceHouse.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
## Étape 2 : Image finale pour l'exécution
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
#WORKDIR /app
##
## Copier le résultat de la publication
#COPY --from=build /app/publish .	
#
## Exposer le port par défaut (Render peut le rediriger)
#EXPOSE 8080
#
### Point d'entrée
#ENTRYPOINT ["dotnet", "SpaceHouse.API.dll"]
#

# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les fichiers projet d'abord (pour mieux utiliser le cache Docker)
COPY ["SpaceHouse.API/SpaceHouse.API.csproj", "SpaceHouse.API/"]
COPY ["SpaceHouse.Application/SpaceHouse.Application.csproj", "SpaceHouse.Application/"]
COPY ["SpaceHouse.Domain/SpaceHouse.Domain.csproj", "SpaceHouse.Domain/"]
COPY ["SpaceHouse.Infrastructure/SpaceHouse.Infrastructure.csproj", "SpaceHouse.Infrastructure/"]

# Restaurer les packages pour tous les projets
RUN dotnet restore "SpaceHouse.API/SpaceHouse.API.csproj"

# Copier le reste du code
COPY . .

# Publier le projet
RUN dotnet publish "SpaceHouse.API/SpaceHouse.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Étape 2 : Image finale
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copier le résultat de la publication
COPY --from=build /app/publish .

# Exposer le port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Point d'entrée
ENTRYPOINT ["dotnet", "SpaceHouse.API.dll"]
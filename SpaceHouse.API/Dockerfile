# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier tous les fichiers de la solution
COPY . .

# Restaurer les packages NuGet
RUN dotnet restore "SpaceHouse.API/SpaceHouse.API.csproj"

# Publier le projet en Release
RUN dotnet publish "SpaceHouse.API/SpaceHouse.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Étape 2 : Image finale pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copier le résultat de la publication
COPY --from=build /app/publish .

# Exposer le port par défaut (Render peut le rediriger)
EXPOSE 8080

# Point d'entrée
ENTRYPOINT ["dotnet", "SpaceHouse.API.dll"]

using SpaceHouse.Application.DTOs;
using SpaceHouse.Application.Interfaces;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.Services
{
    public class ZoneFonctionnelleService : IZoneFonctionnelleService
    {
        // Pour l'instant, nous utilisons un "dictionnaire" codé en dur.
        // Cela simule une base de données des recommandations de la NASA.
        private readonly Dictionary<TypeZoneFonctionnelle, ZoneDeFonctionnementDefautDto> _defaultZoneData = new()
    {
        { TypeZoneFonctionnelle.Sommeil, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Sommeil, Name = "Quartier de sommeil", DefaultWidthM = 2.5, DefaultDepthM = 3.0, DefaultHeightM = 2.2, ColorHex = "#4A90E2" } },
        { TypeZoneFonctionnelle.Exercice, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Exercice, Name = "Salle d'exercice", DefaultWidthM = 4.0, DefaultDepthM = 4.0, DefaultHeightM = 2.5, ColorHex = "#F5A623" } },
        { TypeZoneFonctionnelle.Hygiene, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Hygiene, Name = "Salle d'hygiène", DefaultWidthM = 2.0, DefaultDepthM = 2.5, DefaultHeightM = 2.2, ColorHex = "#7ED321" } },
        { TypeZoneFonctionnelle.PreparationNourriture, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.PreparationNourriture, Name = "Préparation des aliments", DefaultWidthM = 3.0, DefaultDepthM = 3.0, DefaultHeightM = 2.2, ColorHex = "#BD10E0" } },
        { TypeZoneFonctionnelle.Travail, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Travail, Name = "Espace de travail", DefaultWidthM = 3.5, DefaultDepthM = 3.0, DefaultHeightM = 2.2, ColorHex = "#50E3C2" } },
        { TypeZoneFonctionnelle.GestionDéchets, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.GestionDéchets, Name = "Gestion des déchets", DefaultWidthM = 1.5, DefaultDepthM = 2.0, DefaultHeightM = 2.2, ColorHex = "#8B572A" } },
        { TypeZoneFonctionnelle.Médical, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Médical, Name = "Poste médical", DefaultWidthM = 2.5, DefaultDepthM = 2.5, DefaultHeightM = 2.2, ColorHex = "#D0021B" } },
        { TypeZoneFonctionnelle.Loisirs, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Loisirs, Name = "Zone de loisirs", DefaultWidthM = 4.0, DefaultDepthM = 3.5, DefaultHeightM = 2.5, ColorHex = "#F8E71C" } },
        { TypeZoneFonctionnelle.Stockage, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.Stockage, Name = "Stockage", DefaultWidthM = 3.0, DefaultDepthM = 4.0, DefaultHeightM = 2.5, ColorHex = "#B8E986" } },
        { TypeZoneFonctionnelle.SCLVE, new ZoneDeFonctionnementDefautDto { Type = TypeZoneFonctionnelle.SCLVE, Name = "Support Vie (SCLVE)", DefaultWidthM = 2.0, DefaultDepthM = 2.0, DefaultHeightM = 2.2, ColorHex = "#9013FE" } }
    };

        public async Task<IEnumerable<ZoneDeFonctionnementDefautDto>> GetAllDefaultZonesAsync()
        {
            return await Task.FromResult(_defaultZoneData.Values);
        }

        public async Task<ZoneDeFonctionnementDefautDto?> GetDefaultZoneAsync(TypeZoneFonctionnelle type)
        {
            _defaultZoneData.TryGetValue(type, out var zoneData);
            return await Task.FromResult(zoneData);
        }
    }
}

    
using SpaceHouse.Application.DTOs;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.Services
{
    public class ValidationService
    {
        public async Task<List<ValidationResultDto>> ValidateHabitatAsync(ValidateHabitatRequestDto request)
        {

            var results = new List<ValidationResultDto>();

            // 1. Valider le volume total
            await ValidateTotalVolume(request, results);

            // 2. Valider le chevauchement des zones
            await ValidateZoneOverlap(request, results);

            // 3. Valider que les zones sont dans l'habitat
            await ValidateZoneContainment(request, results);

            //4. Val
            await ValidateFunctionalRequirements(request, results);

            await ValidateMissionConstraints(request, results);

            return await Task.FromResult(results);
        }

        private const double DAILY_STORAGE_VOLUME_PER_PERSON_M3 = 0.1;



        private async Task ValidateTotalVolume(ValidateHabitatRequestDto request, List<ValidationResultDto> results)
        {
            double habitatVolume = 0;
            if (request.HabitatShape == FormeHabitat.Cylindre)
            {
                habitatVolume = Math.PI * Math.Pow(request.HabitatRadius, 2) * request.HabitatHeight;

            }
            else if (request.HabitatShape == FormeHabitat.Sphère)
            {
                habitatVolume = (4.0 / 3.0) * Math.PI * Math.Pow(request.HabitatRadius, 3);
            }
            double totalZonesVolume = request.Zones.Sum(z => z.WidthM * z.DepthM * z.HeightM);

            if (totalZonesVolume > habitatVolume)
            {
                results.Add(new ValidationResultDto
                {
                    IsValid = false,
                    Message = $"Le volume total des zones ({totalZonesVolume:F2} m³) dépasse le volume de l'habitat ({habitatVolume:F2} m³)."
                });
            }
        }

        private async Task ValidateZoneOverlap(ValidateHabitatRequestDto request, List<ValidationResultDto> results)
        {
            for (int i = 0; i < request.Zones.Count; i++)
            {
                for (int j = i + 1; j < request.Zones.Count; j++)
                {
                    var z1 = request.Zones[i];
                    var z2 = request.Zones[j];

                    if (BoxesOverlap(z1, z2))
                    {
                        results.Add(new ValidationResultDto
                        {
                            IsValid = false,
                            Message = $"La zone '{z1.Name}' chevauche la zone '{z2.Name}'.",
                            ZoneId = z1.Id
                        });
                        results.Add(new ValidationResultDto
                        {
                            IsValid = false,
                            Message = $"La zone '{z2.Name}' chevauche la zone '{z1.Name}'.",
                            ZoneId = z2.Id
                        });
                    }
                    else
                    {
                        ValidateAdjacencyRules(z1, z2, results);
                    }
                }
            }
        }

        private void ValidateAdjacencyRules(FunctionalZoneDto zoneA, FunctionalZoneDto zoneB, List<ValidationResultDto> results)
        {
            double distance = Math.Sqrt(
                Math.Pow(zoneA.PositionX - zoneB.PositionX, 2) +
                Math.Pow(zoneA.PositionY - zoneB.PositionY, 2) +
                Math.Pow(zoneA.PositionZ - zoneB.PositionZ, 2)
            );

            double proximityThreshold = (zoneA.WidthM + zoneB.WidthM) / 2 + 1.0;

            if (distance < proximityThreshold)
            {
                // Règle 1 : Sommeil vs Exercice
                bool isSleepNextToExercise =
                    (zoneA.Type == TypeZoneFonctionnelle.Sommeil && zoneB.Type == TypeZoneFonctionnelle.Exercice) ||
                    (zoneB.Type == TypeZoneFonctionnelle.Sommeil && zoneA.Type == TypeZoneFonctionnelle.Exercice);

                if (isSleepNextToExercise)
                {
                    results.Add(new ValidationResultDto
                    {
                        IsValid = true, // Avertissement
                        Message = "Avertissement de voisinage : La zone de Sommeil est adjacente à la zone d'Exercice (bruyante).",
                        ZoneId = zoneA.Id
                    });
                }
            }
        }

        private bool BoxesOverlap(FunctionalZoneDto a, FunctionalZoneDto b)
        {
            // Calcule les bornes de chaque boîte
            var aMin = new { x = a.PositionX - a.WidthM / 2, y = a.PositionY, z = a.PositionZ - a.DepthM / 2 };
            var aMax = new { x = a.PositionX + a.WidthM / 2, y = a.PositionY + a.HeightM, z = a.PositionZ + a.DepthM / 2 };
            var bMin = new { x = b.PositionX - b.WidthM / 2, y = b.PositionY, z = b.PositionZ - b.DepthM / 2 };
            var bMax = new { x = b.PositionX + b.WidthM / 2, y = b.PositionY + b.HeightM, z = b.PositionZ + b.DepthM / 2 };

            return (aMin.x <= bMax.x && aMax.x >= bMin.x) &&
                   (aMin.y <= bMax.y && aMax.y >= bMin.y) &&
                   (aMin.z <= bMax.z && aMax.z >= bMin.z);
        }

        private async Task ValidateZoneContainment(ValidateHabitatRequestDto request, List<ValidationResultDto> results)
        {
            foreach (var zone in request.Zones)
            {
                // Vérifier si les coins de la boîte sont à l'intérieur du cylindre
                var halfWidth = zone.WidthM / 2;
                var halfDepth = zone.DepthM / 2;
                var corners = new[]
                {
                new { x = zone.PositionX - halfWidth, z = zone.PositionZ - halfDepth },
                new { x = zone.PositionX + halfWidth, z = zone.PositionZ - halfDepth },
                new { x = zone.PositionX - halfWidth, z = zone.PositionZ + halfDepth },
                new { x = zone.PositionX + halfWidth, z = zone.PositionZ + halfDepth },
            };

                foreach (var corner in corners)
                {
                    // Vérifier si le coin est en dehors du cercle de la base du cylindre
                    if (Math.Pow(corner.x, 2) + Math.Pow(corner.z, 2) > Math.Pow(request.HabitatRadius, 2))
                    {
                        results.Add(new ValidationResultDto
                        {
                            IsValid = false,
                            Message = $"La zone '{zone.Name}' dépasse des limites de l'habitat.",
                            ZoneId = zone.Id
                        });
                        break; // Pas besoin de vérifier les autres coins
                    }
                }

                // Vérifier si la hauteur de la zone dépasse celle de l'habitat
                if (zone.PositionY < 0 || (zone.PositionY + zone.HeightM) > request.HabitatHeight)
                {
                    results.Add(new ValidationResultDto
                    {
                        IsValid = false,
                        Message = $"La zone '{zone.Name}' dépasse la hauteur de l'habitat.",
                        ZoneId = zone.Id
                    });
                }
            }
        }

        private async Task ValidateFunctionalRequirements(ValidateHabitatRequestDto request, List<ValidationResultDto> results)
        {
            // Règle 1 : Valider l'espace de sommeil
            // Recommandation (simplifiée) : 5 m³ de volume personnel par membre d'équipage.
            const double requiredVolumePerCrewMember = 5.0;
            double totalRequiredSleepVolume = request.CrewSize * requiredVolumePerCrewMember;

            // Trouver toutes les zones de sommeil et calculer leur volume total
            var sleepZones = request.Zones.Where(z => z.Type == TypeZoneFonctionnelle.Sommeil).ToList();
            double totalSleepVolume = sleepZones.Sum(z => z.WidthM * z.DepthM * z.HeightM);

            if (sleepZones.Any() && totalSleepVolume < totalRequiredSleepVolume)
            {
                // L'erreur est appliquée à toutes les zones de sommeil
                foreach (var zone in sleepZones)
                {
                    results.Add(new ValidationResultDto
                    {
                        IsValid = false,
                        Message = $"Le volume total pour le sommeil ({totalSleepVolume:F2} m³) est insuffisant. Requis : {totalRequiredSleepVolume:F2} m³ pour {request.CrewSize} membres d'équipage.",
                        ZoneId = zone.Id
                    });
                }
            }

            // Vous pouvez ajouter d'autres règles ici. Par exemple, pour l'exercice :
            // Règle 2 : S'assurer qu'il y a au moins une zone d'exercice pour les missions de plus de 30 jours.
            if (request.MissionDuration > 30 && !request.Zones.Any(z => z.Type == TypeZoneFonctionnelle.Exercice))
            {
                results.Add(new ValidationResultDto
                {
                    IsValid = false,
                    Message = "Une zone d'exercice est requise pour les missions de plus de 30 jours."
                    // Pas de ZoneId car l'erreur est globale
                });
            }
        }

        private async Task ValidateMissionConstraints(ValidateHabitatRequestDto request, List<ValidationResultDto> results)
        {
            // Règle 1: Si l'équipage est > 2, une zone d'exercice est recommandée.
            if (request.CrewSize > 2)
            {
                bool hasExerciseZone = request.Zones.Any(z => z.Type == TypeZoneFonctionnelle.Exercice);
                if (!hasExerciseZone)
                {
                    results.Add(new ValidationResultDto
                    {
                        IsValid = true, // C'est un avertissement, pas une erreur bloquante
                        Message = $"Avertissement : Pour un équipage de {request.CrewSize} membres, une zone d'exercice est fortement recommandée pour la santé physique et mentale."
                        // Pas de ZoneId car ça concerne l'habitat entier.
                    });
                }
            }

            // Règle 2: Exemple pour la destination
            if (request.Destination == Destination.SurfaceLunaire || request.Destination == Destination.SurfaceMartienne)
            {
                // On pourrait vérifier ici si l'habitat est de type ISRU ou a un blindage suffisant, etc.
                // Pour l'instant, on met juste un message d'information.
                results.Add(new ValidationResultDto
                {
                    IsValid = true,
                    Message = "Info : Les habitats en surface nécessitent une protection contre les radiations et les micrométéorites."
                });
            }
            if (request.MissionDurationInDays > 0)
            {
                double requiredStorageVolume = request.CrewSize * request.MissionDurationInDays * DAILY_STORAGE_VOLUME_PER_PERSON_M3;
                double currentStorageVolume = request.Zones
                    .Where(z => z.Type == TypeZoneFonctionnelle.Stockage)
                    .Sum(z => z.WidthM * z.DepthM * z.HeightM);

                if (currentStorageVolume < requiredStorageVolume)
                {
                    results.Add(new ValidationResultDto
                    {
                        IsValid = false, // C'est une erreur critique
                        Message = $"Le volume de stockage ({currentStorageVolume:F2} m³) est insuffisant. Un volume de {requiredStorageVolume:F2} m³ est requis pour {request.CrewSize} membres d'équipage pendant {request.MissionDurationInDays} jours."
                    });
                }
            }
        }
    }
    
}


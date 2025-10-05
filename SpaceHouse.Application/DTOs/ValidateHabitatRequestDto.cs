using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.DTOs
{
    public class ValidateHabitatRequestDto
    {
        public int MissionDurationInDays { get; set; }
        public Destination Destination { get; set; }
        public FormeHabitat HabitatShape { get; set; } // <-- AJOUTEZ CETTE PROPRIÉTÉ
        public double HabitatRadius { get; set; }
        public double HabitatHeight { get; set; }
        public List<FunctionalZoneDto> Zones { get; set; } = new();
        public int CrewSize { get; set; }
        public int MissionDuration { get; set; }
    }

    // DTO pour une zone fonctionnelle simple
    public class FunctionalZoneDto
    {
        public string Id { get; set; } = string.Empty;
        public TypeZoneFonctionnelle Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public double WidthM { get; set; }
        public double DepthM { get; set; }
        public double HeightM { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
    }
}

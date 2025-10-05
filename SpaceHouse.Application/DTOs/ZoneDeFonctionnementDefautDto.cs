using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.DTOs
{
    public class ZoneDeFonctionnementDefautDto
    {
        public TypeZoneFonctionnelle  Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DefaultWidthM { get; set; }
        public double DefaultDepthM { get; set; }
        public double DefaultHeightM { get; set; }
        public string ColorHex { get; set; } = "#FFFFFF"; // Couleur pour la visualisation
    }
}


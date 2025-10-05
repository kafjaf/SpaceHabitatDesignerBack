using SpaceHouse.Domain.Abstraction;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Domain.Entites
{
    public class ZoneFonctionnelle : Entity
    {
        public ZoneFonctionnelle() : base(Guid.NewGuid())
        {
            // Constructeur par défaut
        }
        public TypeZoneFonctionnelle Type { get; set; }
        public string Name { get; set; } = string.Empty;

        // Dimensions
        public double WidthM { get; set; }  // Largeur
        public double DepthM { get; set; }  // Profondeur
        public double HeightM { get; set; } // Hauteur

        // Position 3D à l'intérieur de l'habitat
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }



        // Propriétés calculées
        public double VolumeM3 => WidthM * DepthM * HeightM;
        public double FloorAreaM2 => WidthM * DepthM;

        // Exigences minimales
        public double RequiredVolumeM3 { get; set; } // Volume requis en mètres cubes
        public double RequiredFloorAreaM2 { get; set; } // Surface au sol requise en mètres carrés
    }
}

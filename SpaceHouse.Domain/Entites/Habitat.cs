using System.Reflection;
using SpaceHouse.Domain.Abstraction;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Domain.Entites
{
    public class Habitat : Entity
    {


        //private Habitat(Guid id, string nom, FormeHabitat forme, double dimensions) : base(id)
        //{
        //    Nom = nom;
        //    Forme = forme;
        //    Dimensions = dimensions;
        //}

        /// <summary>
        /// Nom de l'habitat (ex. : "Base Alpha" ou "Habitat Lunaire 01").
        /// </summary>
        public string Nom { get; set; } = default!;

        /// <summary>
        /// Forme géométrique de l'habitat (ex. : sphère, cylindre, dôme, etc.).
        /// </summary>
        public FormeHabitat Forme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TypeHabitat Type { get; set; }

        /// <summary>
        /// Dimensions de l'habitat.
        /// Pour une sphère : représente le rayon.
        /// Pour un cylindre : représente un couple (rayon, hauteur) sérialisé en JSON.
        /// </summary>
        public double Dimensions { get; set; }

        /// <summary>
        /// Liste des modules associés à cet habitat (ex. : module de vie, laboratoire, stockage, etc.).
        /// </summary>
        public List<Module> Modules { get; set; } = new();
    }
}

using SpaceHouse.Application.DTOs;
using SpaceHouse.Application.Interfaces;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.Services
{
    public class HabitatService : IHabitatService
    {
        public async Task<IEnumerable<HabitatTypeDto>> GetHabitatTypesAsync()
        {
            // Pour l'instant, nous retournons des données codées en dur.
            // Plus tard, cela pourrait venir d'une base de données ou d'un fichier de configuration.
            return await Task.FromResult(Enum.GetValues(typeof(TypeHabitat))
                .Cast<TypeHabitat>()
                .Select(e => new HabitatTypeDto
                {
                    Value = (int)e,
                    Name = e.ToString()
                }).ToList()); // Added ToList() to convert IEnumerable to List
        }
    }
}

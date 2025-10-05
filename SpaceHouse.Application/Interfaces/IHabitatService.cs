using SpaceHouse.Application.DTOs;

namespace SpaceHouse.Application.Interfaces
{
    public interface IHabitatService
    {
        Task<IEnumerable<HabitatTypeDto>> GetHabitatTypesAsync();
    }
}

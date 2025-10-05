using SpaceHouse.Application.DTOs;
using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Application.Interfaces
{
    public interface IZoneFonctionnelleService
    {
        Task<IEnumerable<ZoneDeFonctionnementDefautDto>> GetAllDefaultZonesAsync();
        Task<ZoneDeFonctionnementDefautDto?> GetDefaultZoneAsync(TypeZoneFonctionnelle type);
    }
}

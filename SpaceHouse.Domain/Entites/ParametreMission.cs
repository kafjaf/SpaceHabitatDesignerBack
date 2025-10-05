using SpaceHouse.Domain.Enums;

namespace SpaceHouse.Domain.Entites
{
    public record ParametreMission
    (
    int TailleEquipage,
    int DureeMissionEnJours,
    Destination Destination
    );
}

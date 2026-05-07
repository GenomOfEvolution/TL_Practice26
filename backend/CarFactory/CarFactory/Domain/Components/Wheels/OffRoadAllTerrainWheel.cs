using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Wheels;

public class OffRoadAllTerrainWheel : IWheel
{
    public string Name => "Обычные колеса";
    public float RadiusMeters => 0.388f;
}

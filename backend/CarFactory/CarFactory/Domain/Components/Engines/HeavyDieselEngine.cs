using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Engines;

public class HeavyDieselEngine : IEngine
{
    public string Name => "Турбодизель (грузовой)";
    public float MaxRPM => 2500f;
}
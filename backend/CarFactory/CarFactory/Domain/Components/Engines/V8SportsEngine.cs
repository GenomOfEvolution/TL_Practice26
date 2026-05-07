using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Engines;

public class V8SportsEngine : IEngine
{
    public string Name => "Бензиновый V8 (спортивный)";
    public float MaxRPM => 8500f;
}
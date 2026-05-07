using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Engines;

public class ElectricTractionMotor : IEngine
{
    public string Name => "Электрический синхронный мотор";
    public float MaxRPM => 16000f;
}

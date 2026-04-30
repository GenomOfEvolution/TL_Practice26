using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Wheels;

public class CommercialTruckWheel : IWheel
{
    public string Name => "Колеса грузовика";
    public float RadiusMeters => 0.538f;
}

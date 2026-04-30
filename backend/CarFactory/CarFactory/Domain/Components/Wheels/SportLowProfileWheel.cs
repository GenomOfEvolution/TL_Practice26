using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Wheels;

public class SportLowProfileWheel : IWheel
{
    public string Name => "Спортивные колеса";
    public float RadiusMeters => 0.327f;
}

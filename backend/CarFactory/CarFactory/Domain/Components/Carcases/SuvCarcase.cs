using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Carcases;

public class SuvCarcase : ICarcase
{
    public string Name => "Внедорожник";
    public float DragCoefficient => 0.35f;
}
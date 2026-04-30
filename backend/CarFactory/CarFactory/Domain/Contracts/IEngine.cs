namespace CarFactory.Domain.Contracts;

public interface IEngine
{
    string Name { get; }
    float MaxRPM { get; }
}
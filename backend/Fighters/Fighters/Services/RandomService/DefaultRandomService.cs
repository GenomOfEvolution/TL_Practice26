using Fighters.Services.RandomService;

public class DefaultRandomService : IRandomService
{
    public int Next( int minValue, int maxValue ) => Random.Shared.Next( minValue, maxValue );
    public double NextDouble() => Random.Shared.NextDouble();
}
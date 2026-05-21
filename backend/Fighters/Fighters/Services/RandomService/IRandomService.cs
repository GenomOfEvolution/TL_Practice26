namespace Fighters.Services.RandomService;

public interface IRandomService
{
    int Next( int minValue, int maxValue );
    double NextDouble();
}

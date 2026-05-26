namespace Fighters.Models.PointsBudget;

public interface IPointsBudget
{
    int RemainingPoints { get; }
    bool TrySpend( int amount );
}

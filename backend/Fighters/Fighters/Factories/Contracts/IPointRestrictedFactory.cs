using Fighters.Models.PointsBudget;

namespace Fighters.Factories.Contracts;

public interface IPointRestrictedFactory<T> : IFighterComponentFactory<T>
{
    void SetBudget( IPointsBudget budget );
    int RemainingPoints { get; }
    bool TryCreate( int choice, out T? item );
}
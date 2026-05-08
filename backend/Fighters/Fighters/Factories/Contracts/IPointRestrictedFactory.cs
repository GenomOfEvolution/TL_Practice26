namespace Fighters.Factories.Contracts;

public interface IPointRestrictedFactory<T> : IFighterComponentFactory<T>
{
    int RemainingPoints { get; }
    bool TryCreate( int choice, out T? item );
}
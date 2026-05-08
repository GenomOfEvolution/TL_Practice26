namespace Fighters.Factories.Contracts;

public interface IFighterComponentFactory<T>
{
    T Create( int choice );
    void PrintMenu();
}
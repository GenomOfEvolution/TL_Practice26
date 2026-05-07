namespace CarFactory.Factory.CarPartFactory;

public interface ICarPartFactory<T>
{
    T Create( int choice );
    void PrintMenu();
}

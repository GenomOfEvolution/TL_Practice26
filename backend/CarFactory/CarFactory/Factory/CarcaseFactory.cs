using CarFactory.Domain.Components.Carcases;
using CarFactory.Domain.Contracts;

public static class CarcaseFactory
{
    public static ICarcase Create( int choice ) => choice switch
    {
        1 => new SedanCarcase(),
        2 => new SportcarCarcase(),
        3 => new SuvCarcase(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Неверный выбор кузова" )
    };

    public static void PrintMenu()
    {
        Console.WriteLine( "\n=== Выберите тип кузова ===" );
        Console.WriteLine( "1. Седан" );
        Console.WriteLine( "2. Спортивный" );
        Console.WriteLine( "3. Грузовой" );
    }
}
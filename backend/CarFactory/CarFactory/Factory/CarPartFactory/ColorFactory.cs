using CarFactory.Domain.Contracts;
using CarFactory.Domain.Components.Colors;

namespace CarFactory.Factory.CarPartFactory;

public class ColorFactory : ICarPartFactory<IColor>
{
    public IColor Create( int choice ) => choice switch
    {
        1 => new BlackColor(),
        2 => new WhiteColor(),
        3 => new GrayColor(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Неверный выбор цвета" )
    };

    public void PrintMenu()
    {
        Console.WriteLine( "\n=== Выберите цвет ===" );
        Console.WriteLine( "1. Чёрный" );
        Console.WriteLine( "2. Белый" );
        Console.WriteLine( "3. Серый" );
    }
}
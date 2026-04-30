using CarFactory.Domain.Components.Wheels;
using CarFactory.Domain.Contracts;

namespace CarFactory.Factory;

public static class WheelFactory
{
    public static IWheel Create( int choice ) => choice switch
    {
        1 => new SportLowProfileWheel(),
        2 => new OffRoadAllTerrainWheel(),
        3 => new CommercialTruckWheel(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Неверный выбор колёс" )
    };

    public static void PrintMenu()
    {
        Console.WriteLine( "\n=== Выберите колёса ===" );
        Console.WriteLine( "1. Низкий профиль" );
        Console.WriteLine( "2. Обычные" );
        Console.WriteLine( "3. Грузовые" );
    }
}
using CarFactory.Domain.Components.Gearboxes;
using CarFactory.Domain.Contracts;

namespace CarFactory.Factory;

public static class GearboxFactory
{
    public static IGearbox Create( int choice ) => choice switch
    {
        1 => new SportsManualGearbox(),
        2 => new EVSingleSpeedReducer(),
        3 => new HeavyTruckGearbox(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Неверный выбор КПП" )
    };

    public static void PrintMenu()
    {
        Console.WriteLine( "\n=== Выберите коробку передач ===" );
        Console.WriteLine( "1. Спортивная МКПП" );
        Console.WriteLine( "2. Электро-редуктор" );
        Console.WriteLine( "3. Грузовая КПП" );
    }
}
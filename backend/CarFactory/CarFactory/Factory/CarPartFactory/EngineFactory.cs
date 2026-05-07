using CarFactory.Domain.Components.Engines;
using CarFactory.Domain.Contracts;

namespace CarFactory.Factory.CarPartFactory;

public class EngineFactory : ICarPartFactory<IEngine>
{
    public IEngine Create( int choice ) => choice switch
    {
        1 => new V8SportsEngine(),
        2 => new ElectricTractionMotor(),
        3 => new HeavyDieselEngine(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Неверный выбор двигателя" )
    };

    public void PrintMenu()
    {
        Console.WriteLine( "\n=== Выберите двигатель ===" );
        Console.WriteLine( "1. Бензиновый V8 (спорт) — 8500 об/мин" );
        Console.WriteLine( "2. Электрический мотор — 16000 об/мин" );
        Console.WriteLine( "3. Турбодизель (грузовой) — 2500 об/мин" );
    }
}
using CarFactory.Domain.Components.Carcases;
using CarFactory.Domain.Components.Colors;
using CarFactory.Domain.Components.Engines;
using CarFactory.Domain.Components.Gearboxes;
using CarFactory.Domain.Components.Wheels;
using CarFactory.Domain.Contracts;
using CarFactory.Domain.Models;

namespace CarFactory.Factory;

public enum CarPreset
{
    SportCar = 0,
    Electrical,
    Truck,
};

public static class VehicleFactory
{
    private const int _minUserInput = 1;
    private const int _maxUserInput = 3;
    public static Car BuildFromUserInput()
    {
        ColorFactory.PrintMenu();
        IColor color = ColorFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        CarcaseFactory.PrintMenu();
        ICarcase carcase = CarcaseFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        EngineFactory.PrintMenu();
        IEngine engine = EngineFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        GearboxFactory.PrintMenu();
        IGearbox gearbox = GearboxFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        WheelFactory.PrintMenu();
        IWheel wheels = WheelFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        return new Car(
            color,
            carcase,
            engine,
            gearbox,
            wheels );
    }

    public static Car BuildPreset( CarPreset preset ) => preset switch
    {
        CarPreset.SportCar => new Car(
           new WhiteColor(),
           new SportcarCarcase(),
           new V8SportsEngine(),
           new SportsManualGearbox(),
           new SportLowProfileWheel() ),

        CarPreset.Electrical => new Car(
            new GrayColor(),
            new SedanCarcase(),
            new ElectricTractionMotor(),
            new EVSingleSpeedReducer(),
            new SportLowProfileWheel() ),

        CarPreset.Truck => new Car(
            new BlackColor(),
            new SuvCarcase(),
            new HeavyDieselEngine(),
            new HeavyTruckGearbox(),
            new CommercialTruckWheel() ),

        _ => throw new ArgumentException( $"Неизвестная конфигурация: {preset}", nameof( preset ) )
    };

    private static int ReadChoice( int min, int max )
    {
        int choice;
        while ( !TryReadValidChoice( min, max, out choice ) )
        {
            Console.Write( "Неверный ввод. " );
        }
        return choice;
    }

    private static bool TryReadValidChoice( int min, int max, out int choice )
    {
        Console.Write( $"\nВаш выбор ({min}-{max}): " );
        string input = Console.ReadLine() ?? string.Empty;

        return int.TryParse( input, out choice ) && choice >= min && choice <= max;
    }
}
using CarFactory.Domain.Components.Carcases;
using CarFactory.Domain.Components.Colors;
using CarFactory.Domain.Components.Engines;
using CarFactory.Domain.Components.Gearboxes;
using CarFactory.Domain.Components.Wheels;
using CarFactory.Domain.Models;

namespace CarFactory.Factory;

public static class VehicleFactory
{
    public static Car BuildFromUserInput()
    {
        ColorFactory.PrintMenu();
        var color = ColorFactory.Create( ReadChoice( 1, 3 ) );

        CarcaseFactory.PrintMenu();
        var carcase = CarcaseFactory.Create( ReadChoice( 1, 3 ) );

        EngineFactory.PrintMenu();
        var engine = EngineFactory.Create( ReadChoice( 1, 3 ) );

        GearboxFactory.PrintMenu();
        var gearbox = GearboxFactory.Create( ReadChoice( 1, 3 ) );

        WheelFactory.PrintMenu();
        var wheels = WheelFactory.Create( ReadChoice( 1, 3 ) );

        return new Car( color, carcase, engine, gearbox, wheels );
    }

    private static int ReadChoice( int min, int max )
    {
        while ( true )
        {
            Console.Write( $"\nВаш выбор ({min}-{max}): " );
            if ( int.TryParse( Console.ReadLine(), out int choice ) && choice >= min && choice <= max )
                return choice;
            Console.Write( "Неверный ввод. " );
        }
    }

    public static Car BuildPreset( string preset ) => preset.ToLower() switch
    {
        "sport" => new Car(
            new WhiteColor(),
            new SportcarCarcase(),
            new V8SportsEngine(),
            new SportsManualGearbox(),
            new SportLowProfileWheel() ),

        "ev" => new Car(
            new GrayColor(),
            new SedanCarcase(),
            new ElectricTractionMotor(),
            new EVSingleSpeedReducer(),
            new SportLowProfileWheel() ),

        "truck" => new Car(
            new BlackColor(),
            new SuvCarcase(),
            new HeavyDieselEngine(),
            new HeavyTruckGearbox(),
            new CommercialTruckWheel() ),

        _ => throw new ArgumentException( $"Неизвестная конфигурация: {preset}", nameof( preset ) )
    };
}
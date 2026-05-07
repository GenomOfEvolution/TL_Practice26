using CarFactory.Domain.Components.Carcases;
using CarFactory.Domain.Components.Colors;
using CarFactory.Domain.Components.Engines;
using CarFactory.Domain.Components.Gearboxes;
using CarFactory.Domain.Components.Wheels;
using CarFactory.Domain.Contracts;
using CarFactory.Domain.Models;
using CarFactory.Factory.CarPartFactory;

namespace CarFactory.Factory;

public class VehicleFactory
{
    private readonly ICarPartFactory<IColor> _colorFactory;
    private readonly ICarPartFactory<ICarcase> _carcaseFactory;
    private readonly ICarPartFactory<IEngine> _engineFactory;
    private readonly ICarPartFactory<IGearbox> _gearboxFactory;
    private readonly ICarPartFactory<IWheel> _wheelFactory;

    private const int _minUserInput = 1;
    private const int _maxUserInput = 3;

    public VehicleFactory(
        ICarPartFactory<IColor> colorFactory,
        ICarPartFactory<ICarcase> carcaseFactory,
        ICarPartFactory<IEngine> engineFactory,
        ICarPartFactory<IGearbox> gearboxFactory,
        ICarPartFactory<IWheel> wheelFactory )
    {
        _colorFactory = colorFactory;
        _carcaseFactory = carcaseFactory;
        _engineFactory = engineFactory;
        _gearboxFactory = gearboxFactory;
        _wheelFactory = wheelFactory;
    }

    public Car BuildFromUserInput()
    {
        _colorFactory.PrintMenu();
        IColor color = _colorFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        _carcaseFactory.PrintMenu();
        ICarcase carcase = _carcaseFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        _engineFactory.PrintMenu();
        IEngine engine = _engineFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        _gearboxFactory.PrintMenu();
        IGearbox gearbox = _gearboxFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        _wheelFactory.PrintMenu();
        IWheel wheels = _wheelFactory.Create( ReadChoice( _minUserInput, _maxUserInput ) );

        return new Car( color, carcase, engine, gearbox, wheels );
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
using CarFactory.Domain.Models;
using CarFactory.Factory;
using CarFactory.Factory.CarPartFactory;

var vehicleFactory = new VehicleFactory(
    new ColorFactory(),
    new CarcaseFactory(),
    new EngineFactory(),
    new GearboxFactory(),
    new WheelFactory()
);

Console.Write( "Собрать автомобиль вручную? (y/n): " );
Car car = Console.ReadLine()?.Trim().ToLower() == "y"
    ? vehicleFactory.BuildFromUserInput()
    : VehicleFactory.BuildPreset( CarPreset.SportCar );

Console.WriteLine( "Ваша новая машина готова!" );
Console.WriteLine( car.GetInfo() );
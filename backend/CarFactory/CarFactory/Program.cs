using CarFactory.Domain.Models;
using CarFactory.Factory;

Console.Write( "Собрать автомобиль вручную? (y/n): " );
Car car = Console.ReadLine()?.Trim().ToLower() == "y"
    ? VehicleFactory.BuildFromUserInput()
    : VehicleFactory.BuildPreset( "sport" );

Console.WriteLine( "Ваша новая машина готова!" );
Console.WriteLine( car.GetInfo() );
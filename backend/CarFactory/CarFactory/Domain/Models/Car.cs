namespace CarFactory.Domain.Models;

using Domain.Contracts;

public class Car
{
    public IColor Color { get; }
    public ICarcase Carcase { get; }
    public IEngine Engine { get; }
    public IGearbox Gearbox { get; }
    public IWheel Wheels { get; }

    private const float _secondsPerMinute = 60f;
    private const float _mpsToKmh = 3.6f;

    /// <summary>
    /// Максимальная кинематическая скорость в км/ч.
    /// </summary>
    public float MaxSpeed
    {
        get
        {
            var totalRatio = Gearbox.TopGearRatio * Gearbox.FinalDriveRatio;

            if ( Engine.MaxRPM <= 0 || totalRatio <= 0 || Wheels.RadiusMeters <= 0 )
            {
                return 0f;
            }

            float wheelRPM = Engine.MaxRPM / totalRatio;
            float wheelCircumference = 2 * ( float )Math.PI * Wheels.RadiusMeters;
            float speedMetersPerSecond = ( wheelRPM * wheelCircumference ) / _secondsPerMinute;

            return speedMetersPerSecond * _mpsToKmh;
        }
    }

    public Car(
        IColor color,
        ICarcase carcase,
        IEngine engine,
        IGearbox gearbox,
        IWheel wheels )
    {
        Color = color ?? throw new ArgumentNullException( nameof( color ) );
        Carcase = carcase ?? throw new ArgumentNullException( nameof( carcase ) );
        Engine = engine ?? throw new ArgumentNullException( nameof( engine ) );
        Gearbox = gearbox ?? throw new ArgumentNullException( nameof( gearbox ) );
        Wheels = wheels ?? throw new ArgumentNullException( nameof( wheels ) );
    }

    public string GetInfo()
    {
        return
            $"""
            Цвет: {Color.Name};
            Форма кузова: {Carcase.Name};
            Максимальные обороты двигателя: {Engine.MaxRPM};
            Коробка передач: {Gearbox.TopGearRatio};
            Радиус колеса: {Wheels.RadiusMeters} (м);
            Максимальная скорость: {MaxSpeed} (км/ч);
            """;
    }
}
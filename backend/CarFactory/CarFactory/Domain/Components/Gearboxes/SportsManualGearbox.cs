using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Gearboxes;

public class SportsManualGearbox : IGearbox
{
    public float FinalDriveRatio => 3.73f;
    public float TopGearRatio => 0.72f;
}
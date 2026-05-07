using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Gearboxes;

public class HeavyTruckGearbox : IGearbox
{
    public float FinalDriveRatio => 4.88f;
    public float TopGearRatio => 0.78f;
}

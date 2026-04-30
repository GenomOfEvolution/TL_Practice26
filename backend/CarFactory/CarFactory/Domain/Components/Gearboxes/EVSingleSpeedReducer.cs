using CarFactory.Domain.Contracts;

namespace CarFactory.Domain.Components.Gearboxes;

public class EVSingleSpeedReducer : IGearbox
{
    public float FinalDriveRatio => 9.5f;
    public float TopGearRatio => 1.0f;
}

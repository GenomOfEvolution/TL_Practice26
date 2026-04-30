namespace CarFactory.Domain.Contracts;

public interface IGearbox
{
    float FinalDriveRatio { get; } // Главная пара
    float TopGearRatio { get; } // Передаточное число высшей передачи
}
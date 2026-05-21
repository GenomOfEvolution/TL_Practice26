using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Services.DamageService;

public interface IDamageService
{
    /// <summary>
    /// Рассчитывает итоговый урон с учётом статов владельца
    /// </summary>
    DamageStats CalculateAttackDamage( IFighter attacker );

    /// <summary>
    /// Рассчитывает фактический урон после броска кубика и применения брони цели
    /// </summary>
    int CalculateReceivedDamage( DamageStats incomingDamage, IFighter target );
}

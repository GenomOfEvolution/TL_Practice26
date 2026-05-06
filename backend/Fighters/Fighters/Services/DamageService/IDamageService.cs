using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

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

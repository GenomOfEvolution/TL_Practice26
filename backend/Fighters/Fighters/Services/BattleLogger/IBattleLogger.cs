using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Services.BattleLogger;

public interface IBattleLogger
{
    void LogRoundStart();
    void LogRoundEnd();

    void LogAttack( IFighter firstFighter, IFighter secondFighter, DamageStats damage );
    void LogDamageTaken( IFighter damageReciever, int damage );

}

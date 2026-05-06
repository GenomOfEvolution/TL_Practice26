using Fighters.Extensions;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;

namespace Fighters.Models.GameManager;

public class GameManager
{
    private readonly IBattleLogger _battleLogger;
    private readonly IDamageService _damageService;

    public GameManager( IBattleLogger battleLogger, IDamageService damageService )
    {
        _battleLogger = battleLogger;
        _damageService = damageService;
    }

    public IFighter Play( IFighter firstFighter, IFighter secondFighter )
    {
        IFighter turnFirst = firstFighter;
        IFighter turnSecond = secondFighter;

        while ( firstFighter.IsAlive() && secondFighter.IsAlive() )
        {
            _battleLogger.LogRoundStart();

            ExecuteTurn( turnFirst, turnSecond );
            if ( !turnSecond.IsAlive() )
            {
                _battleLogger.LogRoundEnd();
                break;
            }

            ExecuteTurn( turnSecond, turnFirst );
            _battleLogger.LogRoundEnd();
        }

        return firstFighter.IsAlive() ? firstFighter : secondFighter;
    }

    private void ExecuteTurn( IFighter attacker, IFighter defender )
    {
        if ( !attacker.IsAlive() || !defender.IsAlive() ) return;

        DamageStats rawDamage = _damageService.CalculateAttackDamage( attacker );
        _battleLogger.LogAttack( attacker, defender, rawDamage );

        int receivedDamage = _damageService.CalculateReceivedDamage( rawDamage, defender );
        _battleLogger.LogDamageTaken( defender, receivedDamage );

        defender.TakeDamage( receivedDamage );
    }
}

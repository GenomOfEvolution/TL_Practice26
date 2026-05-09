using Fighters.Extensions;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;

namespace Fighters.Models.GameManager;

public class GameManager
{
    private readonly IBattleLogger _battleLogger;
    private readonly IDamageService _damageService;
    private readonly IInitiativeService _initiativeService;

    public GameManager(
        IBattleLogger battleLogger,
        IDamageService damageService,
        IInitiativeService initiativeService )
    {
        _battleLogger = battleLogger;
        _damageService = damageService;
        _initiativeService = initiativeService;
    }

    public IEnumerable<IFighter> Play(
        IEnumerable<IFighter> sideA,
        IEnumerable<IFighter> sideB )
    {
        List<IFighter> fightersA = [ .. sideA ];
        List<IFighter> fightersB = [ .. sideB ];

        if ( !( fightersA.Count > 0 ) || !( fightersB.Count > 0 ) )
        {
            throw new System.ArgumentException(
                "Каждая сторона боя должна содержать хотя бы одного бойца." );
        }

        List<IFighter> allFighters = [ .. fightersA, .. fightersB ];
        HashSet<IFighter> sideASet = [ .. fightersA ];
        HashSet<IFighter> sideBSet = [ .. fightersB ];

        _battleLogger.LogBattleStart( allFighters );
        IReadOnlyList<IFighter> turnOrder = _initiativeService.DetermineTurnOrder( allFighters );
        _battleLogger.LogInitiativeOrder( turnOrder );

        while ( sideASet.Any( f => f.IsAlive() ) && sideBSet.Any( f => f.IsAlive() ) )
        {
            _battleLogger.LogRoundStart();

            for ( int i = 0; i < turnOrder.Count; i++ )
            {
                IFighter attacker = turnOrder[ i ];

                if ( !attacker.IsAlive() )
                {
                    continue;
                }

                HashSet<IFighter> currentSide = sideASet.Contains( attacker ) ? sideASet : sideBSet;
                HashSet<IFighter> enemySide = currentSide == sideASet ? sideBSet : sideASet;
                List<IFighter> validTargets = [ .. enemySide.Where( f => f.IsAlive() ) ];

                if ( validTargets.Count == 0 )
                {
                    break;
                }

                IFighter target = SelectTarget( attacker, validTargets );
                ExecuteTurn( attacker, target );

                if ( !enemySide.Any( f => f.IsAlive() ) )
                {
                    break;
                }
            }

            _battleLogger.LogRoundEnd();
        }

        List<IFighter> winners = allFighters.Where( f => f.IsAlive() ).ToList();
        _battleLogger.LogBattleEnd( winners );

        return winners;
    }

    private void ExecuteTurn( IFighter attacker, IFighter defender )
    {
        if ( !attacker.IsAlive() || !defender.IsAlive() )
        {
            return;
        }

        DamageStats rawDamage = _damageService.CalculateAttackDamage( attacker );
        _battleLogger.LogAttack( attacker, defender, rawDamage );

        int receivedDamage = _damageService.CalculateReceivedDamage( rawDamage, defender );
        _battleLogger.LogDamageTaken( defender, receivedDamage );

        defender.TakeDamage( receivedDamage );
    }

    private static IFighter SelectTarget( IFighter attacker, IList<IFighter> candidates )
    {
        return attacker.Speciality.SelectTarget( candidates );
    }
}
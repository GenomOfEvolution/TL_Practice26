using Fighters.Extensions;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Services.BattleLogger;

public class BattleLogger : IBattleLogger
{
    private const int _borderWidth = 50;
    private int _round = 1;
    private string _roundBorder = String.Empty;

    public void LogAttack( IFighter attacker, IFighter defender, DamageStats damage )
    {
        Console.WriteLine( $"{attacker.Name} атакует {defender.Name} используя {attacker.EquippedWeapon.Name}" );
        Console.WriteLine( $"Расчётный урон: {LogDamage( damage )}!" );
    }

    public void LogDamageTaken( IFighter defender, int damage )
    {
        Console.WriteLine( $"Боец {defender.Name} теряет {damage} здоровья!" );
    }

    public void LogRoundStart()
    {
        string roundText = $" Раунд {_round} ";

        int leftPad = ( _borderWidth - roundText.Length ) / 2;
        int rightPad = _borderWidth - roundText.Length - leftPad;

        _roundBorder = $"{new string( '=', leftPad )}{roundText}{new string( '=', rightPad )}";

        Console.WriteLine( _roundBorder );
    }

    public void LogRoundEnd()
    {
        Console.WriteLine( _roundBorder );
        _round++;
    }

    public void LogWinner( IFighter winner )
    {
        Console.WriteLine( $"Победитель: {winner.Name}!!!" );
    }

    public void LogBattleStart( IEnumerable<IFighter> allFighters )
    {
        Console.WriteLine( "Начало битвы!" );
    }

    public void LogBattleEnd( IEnumerable<IFighter> winners )
    {
        List<IFighter> winnersList = winners.ToList();

        if ( winnersList.Count == 0 )
        {
            Console.WriteLine( "Никто не выжил." );

            return;
        }

        string winnersInfo = String.Join( "\n", winnersList.Select( f => $"- {f.Name} (HP: {f.GetCurrentHealth()}/{f.GetMaxHealth()})" ) );

        Console.WriteLine(
            $"""
            Вот они, наши победители:
            {winnersInfo}
            """
        );
    }

    public void LogInitiativeOrder( IReadOnlyList<IFighter> participants )
    {
        string title = " Инициатива ";
        int leftPad = ( _borderWidth - title.Length ) / 2;
        int rightPad = _borderWidth - title.Length - leftPad;
        string border = $"{new string( '=', leftPad )}{title}{new string( '=', rightPad )}";

        Console.WriteLine( border );
        Console.WriteLine( "Порядок хода:" );

        for ( int i = 0; i < participants.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {participants[ i ].Name}" );
        }

        Console.WriteLine( border );
    }

    private string LogDamage( DamageStats damage )
    {
        return $"{damage.MinDamage} - {damage.MaxDamage} ({damage.Type.GetTypeNameRu()} урон)";
    }
}

using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Services.BattleLogger;

public class BattleLogger : IBattleLogger
{
    private int _round = 1;
    private string _roundBorder = string.Empty;
    private const int BorderWidth = 50;

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

        int leftPad = ( BorderWidth - roundText.Length ) / 2;
        int rightPad = BorderWidth - roundText.Length - leftPad;

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

    public void LogBattleStart( List<IFighter> allFighters )
    {
        Console.WriteLine( "Начало битвы!" );
    }

    public void LogBattleEnd( List<IFighter> winners )
    {
        string winnersInfo = winners == null || winners.Count == 0
       ? "Никто не выжил."
       : string.Join( "\n", winners.Select( f => $"- {f.Name} (HP: {f.GetCurrentHealth()}/{f.GetMaxHealth()})" ) );

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
        int leftPad = ( BorderWidth - title.Length ) / 2;
        int rightPad = BorderWidth - title.Length - leftPad;
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
        return $"{damage.MinDamage} - {damage.MaxDamage} ({GetTypeNameRu( damage.Type )} урон)";
    }

    private static string GetTypeNameRu( DamageType type ) => type switch
    {
        DamageType.Physic => "физический",
        DamageType.Magic => "магический",
        DamageType.Pure => "чистый",
        DamageType.Poison => "токсичный",
        _ => type.ToString()
    };
}

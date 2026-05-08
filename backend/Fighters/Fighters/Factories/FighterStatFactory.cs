using Fighters.Factories.Contracts;
using Fighters.Models.Fighters;
using Fighters.Models.PointsBudget;

namespace Fighters.Factories;

public class FighterStatFactory : IPointRestrictedFactory<FighterStats>
{
    private readonly IPointsBudget _budget;

    public FighterStatFactory( IPointsBudget budget ) => _budget = budget;

    public FighterStats Create( int choice ) =>
        TryCreate( choice, out var stats )
            ? stats
            : throw new InvalidOperationException( "Не удалось создать характеристики" );

    public int RemainingPoints => _budget.RemainingPoints;

    public void PrintMenu()
    {
        Console.WriteLine( $"[Осталось очков: {RemainingPoints}]" );
    }

    public bool TryCreate( int choice, out FighterStats item )
    {
        item = default;

        if ( RemainingPoints <= 0 )
        {
            return false;
        }

        FighterStats stats = CreateStatsInteractive();

        int totalCost = stats.Strength + stats.Dexterity + stats.Intelligence;
        if ( totalCost > RemainingPoints + _spentPoints )
        {
            return false;
        }


        _spentPoints += totalCost;
        item = stats;

        return true;
    }

    private FighterStats CreateStatsInteractive()
    {
        Console.WriteLine( "\nРаспределите очки характеристик:" );

        int strength = GetStatValue( "Сила" );
        int dexterity = GetStatValue( "Ловкость" );
        int intelligence = GetStatValue( "Интеллект" );

        return new FighterStats
        {
            Strength = strength,
            Dexterity = dexterity,
            Intelligence = intelligence
        };
    }

    private int GetStatValue( string statName )
    {
        int points = 0;
        bool isValidInput = false;

        while ( !isValidInput )
        {
            PrintMenu();
            Console.Write( $"Сколько очков потратить на \"{statName}\"? (от 0 до {RemainingPoints}): " );
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out points ) && points >= 0 && points <= RemainingPoints )
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine( "Ошибка ввода. Убедитесь, что это число в указанном диапазоне.\n" );
            }
        }

        return points;
    }
}
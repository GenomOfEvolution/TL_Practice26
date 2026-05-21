using Fighters.Factories.Contracts;
using Fighters.Models.Fighters;
using Fighters.Models.PointsBudget;

namespace Fighters.Factories;

public class FighterStatFactory : IPointRestrictedFactory<FighterStats>
{
    private IPointsBudget _budget = new SharedPointsBudget();

    public int RemainingPoints => _budget.RemainingPoints;

    public FighterStats Create( int choice ) =>
        TryCreate( choice, out var stats )
            ? stats
            : throw new InvalidOperationException( "Не удалось создать характеристики" );

    public void PrintMenu() =>
        Console.WriteLine( $"Осталось очков на всё: {RemainingPoints}" );

    public bool TryCreate( int choice, out FighterStats item )
    {
        item = new FighterStats();

        if ( RemainingPoints <= 0 )
        {
            return false;
        }

        int initialBudget = RemainingPoints;
        FighterStats stats = CreateStatsInteractive( initialBudget );

        int totalCost = stats.Strength + stats.Dexterity + stats.Intelligence;
        if ( totalCost > initialBudget )
        {
            return false;
        }

        if ( !_budget.TrySpend( totalCost ) )
        {
            return false;
        }

        item = stats;

        return true;
    }

    public void SetBudget( IPointsBudget budget )
    {
        _budget = budget;
    }

    private FighterStats CreateStatsInteractive( int initialBudget )
    {
        Console.WriteLine( "Распределите очки характеристик:" );

        PrintMenu();
        int strength = GetStatValue( "Сила", initialBudget );
        int dexterity = GetStatValue( "Ловкость", initialBudget - strength );
        int intelligence = GetStatValue( "Интеллект", initialBudget - strength - dexterity );

        return new FighterStats
        {
            Strength = strength,
            Dexterity = dexterity,
            Intelligence = intelligence
        };
    }

    private int GetStatValue( string statName, int maxAllowed )
    {
        bool isValidInput = false;
        int points = 0;

        while ( !isValidInput )
        {
            Console.Write( $"Сколько очков потратить на \"{statName}\"? (от 0 до {maxAllowed}): " );
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out points ) && points >= 0 && points <= maxAllowed )
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
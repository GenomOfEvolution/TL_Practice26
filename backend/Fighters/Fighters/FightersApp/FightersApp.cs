using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.ItemCatalog;
using Fighters.Models.Weapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.GameManager;
using Fighters.Services.InitiativeService;

namespace Fighters.FightersApp;

public class FightersApp
{
    public static void Run()
    {
        var randService = new DefaultRandomService();
        var damageService = new DamageService( randService );
        var initiativeService = new InitiativeService( randService );
        var gameManager = new GameManager( new BattleLogger(), damageService, initiativeService );

        IItemCatalog<IWeapon> weaponCatalog = new WeaponCatalog();
        IItemCatalog<IArmor> armorCatalog = new ArmorCatalog();

        var factory = new FighterFactory(
            new RaceFactory(),
            new SpecialityFactory(),
            new FighterStatFactory(),
            new WeaponFactory( weaponCatalog ),
            new ArmorFactory( armorCatalog ),
            pointsPerFighter: 100
        );

        string teamAName = ReadTeamName( 1 );
        string teamBName = ReadTeamName( 2, teamAName );

        int teamACount = ReadFighterCount( teamAName );
        int teamBCount = ReadFighterCount( teamBName );

        IFighterTeam teamA = factory.CreateFighterTeam( teamACount );
        IFighterTeam teamB = factory.CreateFighterTeam( teamBCount );

        gameManager.Play( teamA.GetMembers(), teamB.GetMembers() );
    }

    private static string ReadTeamName( int teamNumber, string? existingName = null )
    {
        bool isValid = false;
        string teamName = string.Empty;

        while ( !isValid )
        {
            Console.Write( $"Введите название команды {teamNumber}: " );
            string? input = Console.ReadLine()?.Trim();

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                Console.WriteLine( "Ошибка: название команды не может быть пустым." );
            }
            else if ( existingName != null && input.Equals( existingName, StringComparison.OrdinalIgnoreCase ) )
            {
                Console.WriteLine( "Ошибка: названия команд должны быть разными." );
            }
            else
            {
                teamName = input;
                isValid = true;
            }
        }

        return teamName;
    }

    private static int ReadFighterCount( string teamName )
    {
        int count = 0;
        bool isInputValid = false;

        while ( !isInputValid )
        {
            Console.Write( $"Введите количество бойцов для команды \"{teamName}\" (минимум 1): " );
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out int parsedValue ) && parsedValue > 0 )
            {
                count = parsedValue;
                isInputValid = true;
            }
            else
            {
                Console.WriteLine( "Ошибка: введите положительное целое число." );
            }
        }

        return count;
    }
}
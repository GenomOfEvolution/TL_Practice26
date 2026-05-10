using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.GameManager;
using Fighters.Models.ItemCatalog;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MagicWeapons;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Models.Weapons.RangedWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;

namespace Fighters.FightersApp;

public class FightersApp
{
    public void Run()
    {
        var randService = new DefaultRandomService();
        var damageService = new DamageService( randService );
        var initiativeService = new InitiativeService( randService );
        var gameManager = new GameManager( new BattleLogger(), damageService, initiativeService );

        IItemCatalog<IWeapon> weaponCatalog = new WeaponCatalog(
        [
            // Оружия ближнего боя
            new( new Fists(), 0 ),
            new( new WoodenSword(), 5 ),
            new( new Club(), 8 ),
            new( new Dagger(), 8 ),
            new( new GrassBlade(), 15 ),
            new( new HuntingKnife(), 15 ),
            new( new ReinforcedClub(), 15 ),
            new( new TheSeparator(), 25 ),
            new( new CeremonialKnife(), 25 ),
            new( new DragonSlayer(), 32 ),
            new( new MonoBlade(), 40 ),

            // Оружия дальнего боя
            new( new AlchemicalConcoction(), 10 ),
            new( new Brick(), 13 ),
            new( new CrudeBow(), 6 ),
            new( new GhostRifle(), 42 ),
            new( new GoldenDeagle(), 25 ),
            new( new HeavyCrossbow(), 20 ),
            new( new Longbow(), 14 ),
            new( new Revolver(), 10 ),
            new( new RocketLauncher(), 33 ),
            new( new StarCannnon(), 25 ),

            // Магические оружия
            new( new ApprenticeWand(), 5 ),
            new( new DragonBreath(), 30 ),
            new( new LeafWand(), 15 ),
            new( new Nirvana(), 40 ),
            new( new OakStaff(), 7 ),
            new( new SlitherWand(), 20 ),
            new( new SparkWand(), 15 ),
        ] );

        IItemCatalog<IArmor> armorCatalog = new ArmorCatalog(
        [
            new( new NoArmor(), 0 ),
            new( new BerserkArmor(), 40 ),
            new( new GlassArmor(), 15 ),
            new( new KnightArmor(), 10 ),
            new( new LeatherArmor(), 5 ),
            new( new WitchDoctorArmor(), 35 ),
        ] );

        var factory = new FighterFactory(
            new RaceFactory(),
            new SpecialityFactory(),
            new FighterStatFactory(),
            new WeaponFactory( weaponCatalog ),
            new ArmorFactory( armorCatalog ),
            pointsPerFighter: 100
        );

        int teamACount = ReadFighterCount( "A" );
        int teamBCount = ReadFighterCount( "B" );

        IFighterTeam teamA = factory.CreateFighterTeam( teamACount );
        IFighterTeam teamB = factory.CreateFighterTeam( teamBCount );

        gameManager.Play( teamA.GetMembers(), teamB.GetMembers() );
    }

    private static int ReadFighterCount( string teamName )
    {
        int count = 0;
        bool isInputValid = false;

        while ( !isInputValid )
        {
            Console.Write( $"Введите количество бойцов для команды {teamName} (минимум 1): " );
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
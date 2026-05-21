using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.GameManager;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using Moq;

namespace Fighters.UnitTests;

public class GameManagerTests
{
    [Fact]
    public void Play_TwoEqualFighters_FirstFighterWins()
    {
        // Arrange
        var mockRandom = new Mock<IRandomService>();

        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );
        mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );

        var damageService = new DamageService( mockRandom.Object );
        var intiativeService = new InitiativeService( mockRandom.Object );
        var gameManager = new GameManager( new BattleLogger(), damageService, intiativeService );

        IFighter fighterA = CreateFighter( "FighterA" );
        IFighter fighterB = CreateFighter( "FighterB" );

        // Act
        var winner = gameManager.Play( [ fighterA ], [ fighterB ] );

        // Assert
        Assert.Equal( fighterA.Name, winner.First().Name );
    }

    private SingleFighter CreateFighter( string name ) => new SingleFighter(
        name,
        new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
        new HumanRace(),
        new NoSpeciality(),
        new NoArmor(),
        new Fists()
    );
}
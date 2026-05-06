using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.GameManager;
using Fighters.Models.Races;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
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
        var gameManager = new GameManager( new BattleLogger(), damageService );

        var fighterA = CreateFighter( "FighterA" );
        var fighterB = CreateFighter( "FighterB" );

        // Act
        var winner = gameManager.Play( fighterA, fighterB );

        // Assert
        Assert.Equal( fighterA.Name, winner.Name );
    }

    [Fact]
    public void Play_TwoEqualFighters_SecondFighterDies()
    {
        // Arrange
        var mockRandom = new Mock<IRandomService>();
        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );
        mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );

        var damageService = new DamageService( mockRandom.Object );
        var gameManager = new GameManager( new BattleLogger(), damageService );

        var fighterA = CreateFighter( "FighterA" );
        var fighterB = CreateFighter( "FighterB" );

        // Act
        gameManager.Play( fighterA, fighterB );

        // Assert
        Assert.True( fighterA.GetCurrentHealth() > 0 );
        Assert.Equal( 0, fighterB.GetCurrentHealth() );
    }

    private SingleFighter CreateFighter( string name ) => new SingleFighter(
        name,
        new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
        new HumanRace(),
        new NoArmor(),
        new Fists()
    );
}
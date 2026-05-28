using Fighters.Models.Fighters;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using GameManager = Fighters.Services.GameManager.GameManager;
using Moq;
using TestLibrary;

namespace Fighters.UnitTests.Services;

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

        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        // Act
        var winner = gameManager.Play( [ fighterA ], [ fighterB ] );

        // Assert
        Assert.Equal( fighterA.Name, winner.First().Name );
    }
}
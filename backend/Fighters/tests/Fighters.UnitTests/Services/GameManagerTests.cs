using Fighters.Extensions;
using Fighters.Models.Fighters;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using Fighters.UnitTests.TestHelpers;
using Moq;
using GameManager = Fighters.Services.GameManager.GameManager;

namespace Fighters.UnitTests.Services;

public class GameManagerTests
{
    private readonly Mock<IRandomService> _mockRandom;
    private readonly Mock<IBattleLogger> _mockLogger;

    private readonly GameManager _gameManager;

    public GameManagerTests()
    {
        _mockRandom = new Mock<IRandomService>();
        _mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );
        _mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );

        _mockLogger = new Mock<IBattleLogger>();
        var damageService = new DamageService( _mockRandom.Object );
        var initiativeService = new InitiativeService( _mockRandom.Object );
        _gameManager = new GameManager( _mockLogger.Object, damageService, initiativeService );
    }

    [Fact]
    public void Play_TwoEqualFighters_FirstFighterWins()
    {
        // Arrange
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        // Act
        var winner = _gameManager.Play( [ fighterA ], [ fighterB ] );

        // Assert
        Assert.Equal( fighterA.Name, winner.First().Name );
    }

    [Fact]
    public void Play_FirstFighterDies_SecondFighterWins()
    {
        // Arrange
        FighterStats firstFighterStats = new FighterStats { Dexterity = 1, Intelligence = 1, Strength = 1 };
        FighterStats secondFighterStats = new FighterStats { Dexterity = 50, Intelligence = 50, Strength = 50 };

        var fighterA = FighterBuilder.CreateWithStats( firstFighterStats, "FighterA" );
        var fighterB = FighterBuilder.CreateWithStats( secondFighterStats, "FighterB" );

        // Act
        var winners = _gameManager.Play( [ fighterA ], [ fighterB ] );

        // Assert
        Assert.False( fighterA.IsAlive() );
        Assert.Equal( "FighterB", winners.First().Name );
    }

    [Fact]
    public void Play_MultipleFighters_TeamWithLastAliveWins()
    {
        // Arrange
        FighterStats firstTeamStats = new FighterStats { Dexterity = 10, Intelligence = 10, Strength = 10 };
        FighterStats secondTeamStats = new FighterStats { Dexterity = 1, Intelligence = 1, Strength = 1 };

        IFighter teamAfirstFighter = FighterBuilder.CreateWithStats( firstTeamStats, "TeamA_F1" );
        IFighter teamAsecondFighter = FighterBuilder.CreateWithStats( firstTeamStats, "TeamA_F2" );

        IFighter teamBfirstFighter = FighterBuilder.CreateWithStats( secondTeamStats, "TeamB_F1" );
        IFighter teamBsecondFighter = FighterBuilder.CreateWithStats( secondTeamStats, "TeamB_F2" );

        // Act
        var winners = _gameManager.Play( [ teamAfirstFighter, teamAsecondFighter ], [ teamBfirstFighter, teamBsecondFighter ] );

        // Assert
        Assert.Equal( 2, winners.Count() );
        Assert.Contains( winners, w => w.Name == "TeamA_F1" );
        Assert.Contains( winners, w => w.Name == "TeamA_F2" );
    }
}

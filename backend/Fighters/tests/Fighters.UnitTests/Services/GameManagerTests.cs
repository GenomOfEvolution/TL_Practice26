using Fighters.Extensions;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using GameManager = Fighters.Services.GameManager.GameManager;
using Moq;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Services;

public class GameManagerTests
{
    private readonly Mock<IRandomService> _mockRandom;
    private readonly GameManager _gameManager;
    private readonly Mock<IBattleLogger> _mockLogger;

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
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        var winner = _gameManager.Play( [ fighterA ], [ fighterB ] );

        Assert.Equal( fighterA.Name, winner.First().Name );
    }

    [Fact]
    public void Play_OneFighterPerTeam_ReturnsSingleWinner()
    {
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        var winners = _gameManager.Play( [ fighterA ], [ fighterB ] );

        Assert.Single( winners );
    }

    [Fact]
    public void Play_FighterADies_FighterBWins()
    {
        var fighterA = new SingleFighter(
            "FighterA",
            new FighterStats { Strength = 1, Dexterity = 1, Intelligence = 1 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var fighterB = new SingleFighter(
            "FighterB",
            new FighterStats { Strength = 50, Dexterity = 50, Intelligence = 50 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );

        var winners = _gameManager.Play( [ fighterA ], [ fighterB ] );

        Assert.Single( winners );
        Assert.Equal( "FighterB", winners.First().Name );
    }

    [Fact]
    public void Play_MultipleFighters_TeamWithLastAliveWins()
    {
        IFighter teamA1 = FighterBuilder.CreateDefault( "TeamA_F1" );
        IFighter teamA2 = FighterBuilder.CreateDefault( "TeamA_F2" );
        var teamB1 = new SingleFighter(
            "TeamB_F1",
            new FighterStats { Strength = 1, Dexterity = 1, Intelligence = 1 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var teamB2 = new SingleFighter(
            "TeamB_F2",
            new FighterStats { Strength = 1, Dexterity = 1, Intelligence = 1 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );

        var winners = _gameManager.Play( [ teamA1, teamA2 ], [ teamB1, teamB2 ] );

        Assert.Equal( 2, winners.Count() );
        Assert.Contains( winners, w => w.Name == "TeamA_F1" );
        Assert.Contains( winners, w => w.Name == "TeamA_F2" );
    }

    [Fact]
    public void Play_RoundLogic_ExecutesUntilOneTeamRemains()
    {
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        var winners = _gameManager.Play( [ fighterA ], [ fighterB ] );

        Assert.Single( winners );
        Assert.False( fighterB.IsAlive() );
        Assert.True( fighterA.IsAlive() );
    }

    [Fact]
    public void Play_LoggerCalled_BattleStartAndEnd()
    {
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        _gameManager.Play( [ fighterA ], [ fighterB ] );

        _mockLogger.Verify( l => l.LogBattleStart( It.IsAny<IEnumerable<IFighter>>() ), Times.Once );
        _mockLogger.Verify( l => l.LogBattleEnd( It.IsAny<IEnumerable<IFighter>>() ), Times.Once );
    }
}

using Fighters.Models.Fighters;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using GameManager = Fighters.Services.GameManager.GameManager;
using Moq;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Services;

public class BattleLoggerTests
{
    [Fact]
    public void GameManager_CallsLogBattleStartAndEnd_AtLeastOnce()
    {
        var mockRandom = new Mock<IRandomService>();
        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );
        mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );

        var mockLogger = new Mock<IBattleLogger>();
        var gameManager = new GameManager(
            mockLogger.Object,
            new DamageService( mockRandom.Object ),
            new InitiativeService( mockRandom.Object ) );

        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );

        gameManager.Play( [ fighterA ], [ fighterB ] );

        mockLogger.Verify( l => l.LogBattleStart( It.IsAny<IEnumerable<IFighter>>() ), Times.AtLeastOnce );
        mockLogger.Verify( l => l.LogBattleEnd( It.IsAny<IEnumerable<IFighter>>() ), Times.AtLeastOnce );
        mockLogger.Verify( l => l.LogRoundStart(), Times.AtLeastOnce );
        mockLogger.Verify( l => l.LogRoundEnd(), Times.AtLeastOnce );
        mockLogger.Verify( l => l.LogAttack( It.IsAny<IFighter>(), It.IsAny<IFighter>(), It.IsAny<Fighters.Models.Damage.DamageStats>() ), Times.AtLeastOnce );
        mockLogger.Verify( l => l.LogDamageTaken( It.IsAny<IFighter>(), It.IsAny<int>() ), Times.AtLeastOnce );
    }
}

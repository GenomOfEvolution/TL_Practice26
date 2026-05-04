using Fighters.Models.Fighters;
using Fighters.Models.GameManager;
using Fighters.Models.Races;

namespace Fighters.UnitTests;

public class GameManagerTests
{
    [Fact]
    public void Play_TwoEqualFighters_FirstFighterWins()
    {
        // Arrange 
        var gameManager = new GameManager();
        var fighterA = new Knight( "FighterA", new Human() );
        var fighterB = new Knight( "FighterB", new Human() );

        // Act
        var winner = gameManager.Play( fighterA, fighterB );

        // Asssert
        Assert.Equal( winner.Name, fighterA.Name );
    }

    [Fact]
    public void Play_TwoEqualFighters_SecondFighterDies()
    {
        // Arrange 
        var gameManager = new GameManager();
        var fighterA = new Knight( "FighterA", new Human() );
        var fighterB = new Knight( "FighterB", new Human() );

        // Act
        gameManager.Play( fighterA, fighterB );

        // Asssert
        Assert.True( fighterA.GetCurrentHealth() > 0 );
        Assert.Equal( 0, fighterB.GetCurrentHealth() );
    }
}

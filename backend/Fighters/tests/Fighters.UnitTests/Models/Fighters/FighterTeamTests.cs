using Fighters.Models.Fighters;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Models.Fighters;

public class FighterTeamTests
{
    [Fact]
    public void FighterTeam_AddFighter_MemberAdded()
    {
        // Arrange
        var team = new FighterTeam();
        IFighter fighter = FighterBuilder.CreateDefault( "FighterA" );

        // Act
        team.AddFighter( fighter );

        // Assert
        Assert.Contains( fighter, team.GetMembers() );
    }

    [Fact]
    public void FighterTeam_GetMembers_ReturnsAllAdded()
    {
        // Arrange
        var team = new FighterTeam();
        IFighter fighterA = FighterBuilder.CreateDefault( "FighterA" );
        IFighter fighterB = FighterBuilder.CreateDefault( "FighterB" );
        IFighter fighterC = FighterBuilder.CreateDefault( "FighterC" );

        // Act
        team.AddFighter( fighterA );
        team.AddFighter( fighterB );
        team.AddFighter( fighterC );

        // Assert
        Assert.Equal( 3, team.GetMembers().Count() );
        Assert.Contains( fighterA, team.GetMembers() );
        Assert.Contains( fighterB, team.GetMembers() );
        Assert.Contains( fighterC, team.GetMembers() );
    }
}

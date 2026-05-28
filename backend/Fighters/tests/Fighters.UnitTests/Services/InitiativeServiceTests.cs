using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.InitiativeService;
using Fighters.Services.RandomService;
using Moq;

namespace Fighters.UnitTests.Services;

public class InitiativeServiceTests
{
    private static SingleFighter CreateFighter( string name, int dexterity, IRace race )
    {
        return new SingleFighter(
            name,
            new FighterStats { Strength = 10, Dexterity = dexterity, Intelligence = 10 },
            race,
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
    }

    [Fact]
    public void DetermineTurnOrder_SortedByInitiativeDesc()
    {
        // Arrange
        var mockRandom = new Mock<IRandomService>();
        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );

        var service = new InitiativeService( mockRandom.Object );
        var fighters = new List<IFighter>
        {
            CreateFighter( "Dex10", 10, new HumanRace() ),
            CreateFighter( "Dex11", 11, new HumanRace() ),
            CreateFighter( "Dex12", 12, new HumanRace() ),
            CreateFighter( "Dex13", 13, new HumanRace() ),
            CreateFighter( "Dex14", 14, new HumanRace() ),
        };

        // Act
        var result = service.DetermineTurnOrder( fighters );

        // Assert
        Assert.Equal( "Dex14", result[ 0 ].Name );
        Assert.Equal( "Dex13", result[ 1 ].Name );
        Assert.Equal( "Dex12", result[ 2 ].Name );
        Assert.Equal( "Dex11", result[ 3 ].Name );
        Assert.Equal( "Dex10", result[ 4 ].Name );
    }

    [Fact]
    public void DetermineTurnOrder_RaceModifierApplied()
    {
        // Arrange
        var mockRandom = new Mock<IRandomService>();
        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => max );

        var service = new InitiativeService( mockRandom.Object );
        var fighters = new List<IFighter>
        {
            CreateFighter( "StoneGiant", 10, new StoneGiantRace() ),
            CreateFighter( "Gnome", 10, new GnomeRace() ),
            CreateFighter( "Goblin", 10, new GoblinRace() ),
            CreateFighter( "Human", 10, new HumanRace() ),
            CreateFighter( "Drow", 10, new DrowRace() ),
        };

        // Act
        var result = service.DetermineTurnOrder( fighters );

        // Assert
        Assert.Equal( "Drow", result[ 0 ].Name );
        Assert.Equal( "Human", result[ 1 ].Name );
        Assert.Equal( "Gnome", result[ 2 ].Name );
        Assert.Equal( "Goblin", result[ 3 ].Name );
        Assert.Equal( "StoneGiant", result[ 4 ].Name );
    }

    [Fact]
    public void DetermineTurnOrder_TiebreakerByDexterity()
    {
        // Arrange
        var mockRandom = new Mock<IRandomService>();
        mockRandom
            .Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns( 0 );

        var service = new InitiativeService( mockRandom.Object );
        var fighters = new List<IFighter>
        {
            CreateFighter( "Dex2", 2, new HumanRace() ),
            CreateFighter( "Dex10", 10, new HumanRace() ),
            CreateFighter( "Dex8", 8, new HumanRace() ),
            CreateFighter( "Dex4", 4, new HumanRace() ),
            CreateFighter( "Dex6", 6, new HumanRace() ),
        };

        // Act
        var result = service.DetermineTurnOrder( fighters );

        // Assert
        Assert.Equal( "Dex10", result[ 0 ].Name );
        Assert.Equal( "Dex8", result[ 1 ].Name );
        Assert.Equal( "Dex6", result[ 2 ].Name );
        Assert.Equal( "Dex4", result[ 3 ].Name );
        Assert.Equal( "Dex2", result[ 4 ].Name );
    }
}

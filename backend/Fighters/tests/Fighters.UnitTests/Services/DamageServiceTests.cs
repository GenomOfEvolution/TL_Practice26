using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Models.Weapons.RangedWeapons;
using Fighters.Services.DamageService;
using Fighters.Services.RandomService;
using Moq;

namespace Fighters.UnitTests.Services;

public class DamageServiceTests
{
    private readonly Mock<IRandomService> _mockRandom;

    public DamageServiceTests()
    {
        _mockRandom = new Mock<IRandomService>();
    }

    [Fact]
    public void CalculateAttackDamage_DefaultFighter_ReturnsDamageStats()
    {
        // Arrange
        _mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );
        var service = new DamageService( _mockRandom.Object );
        var fighter = CreateDefaultFighter();

        // Act
        var result = service.CalculateAttackDamage( fighter );

        // Assert
        Assert.NotNull( result );
        Assert.True( result.MinDamage > 0 );
        Assert.True( result.MaxDamage >= result.MinDamage );
    }

    [Fact]
    public void CalculateAttackDamage_CritRoll_AppliesCritDamage()
    {
        // Arrange
        _mockRandom.Setup( r => r.NextDouble() ).Returns( 0.0 );
        var service = new DamageService( _mockRandom.Object );
        var fighter = CreateDefaultFighter();

        // Act
        var result = service.CalculateAttackDamage( fighter );

        // Assert
        Assert.True( result.MinDamage > 0 );
        Assert.True( result.MaxDamage >= result.MinDamage );
    }

    [Fact]
    public void CalculateAttackDamage_DrowRace_AppliesRaceModifier()
    {
        // Arrange
        _mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );
        var service = new DamageService( _mockRandom.Object );
        var fighter = new SingleFighter(
            "Drow",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new DrowRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Brick()
        );

        // Act
        var result = service.CalculateAttackDamage( fighter );

        // Assert
        var expectedBase = new DamageService( _mockRandom.Object ).CalculateAttackDamage(
            new SingleFighter(
                "Human",
                new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
                new HumanRace(),
                new NoSpeciality(),
                new NoArmor(),
                new Brick()
            )
        );

        Assert.True( result.MinDamage > expectedBase.MinDamage );
        Assert.True( result.MaxDamage > expectedBase.MaxDamage );
    }

    [Fact]
    public void CalculateAttackDamage_GlassArmor_AppliesArmorModifier()
    {
        // Arrange
        _mockRandom.Setup( r => r.NextDouble() ).Returns( 1.0 );
        var service = new DamageService( _mockRandom.Object );
        var fighter = new SingleFighter(
            "GlassUser",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new GlassArmor(),
            new Fists()
        );

        var normalFighter = new SingleFighter(
            "Normal",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );

        // Act
        var result = service.CalculateAttackDamage( fighter );
        var normalResult = service.CalculateAttackDamage( normalFighter );

        // Assert
        Assert.True( result.MinDamage > normalResult.MinDamage );
        Assert.True( result.MaxDamage > normalResult.MaxDamage );
    }

    [Fact]
    public void CalculateReceivedDamage_KnightArmor_ResistancesReduceDamage()
    {
        // Arrange
        _mockRandom.Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => min );
        var service = new DamageService( _mockRandom.Object );
        var knight = new SingleFighter(
            "Knight",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new KnightArmor(),
            new Fists()
        );
        var incomingDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0,
            CritDamage = 1,
        };

        // Act
        var result = service.CalculateReceivedDamage( incomingDamage, knight );

        // Assert
        int expectedMin = ( int )( 100 * ( 1 - 0.40f ) );
        Assert.Equal( expectedMin, result );
    }

    [Fact]
    public void CalculateReceivedDamage_ValidRange_ReturnsDamageInRange()
    {
        // Arrange
        _mockRandom.Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) =>
            {
                Assert.True( max > min );
                return min;
            } );
        var service = new DamageService( _mockRandom.Object );
        var fighter = CreateDefaultFighter();
        var incomingDamage = new DamageStats
        {
            MinDamage = 50,
            MaxDamage = 100,
            Type = DamageType.Physic,
            CritChance = 0,
            CritDamage = 1,
        };

        // Act
        var result = service.CalculateReceivedDamage( incomingDamage, fighter );

        // Assert
        Assert.InRange( result, 1, 100 );
    }

    [Fact]
    public void CalculateReceivedDamage_StoneGiantRace_AppliesIncomingModifier()
    {
        // Arrange
        _mockRandom.Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) )
            .Returns<int, int>( ( min, max ) => min );
        var service = new DamageService( _mockRandom.Object );
        var stoneGiant = new SingleFighter(
            "StoneGiant",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new StoneGiantRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var incomingDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0,
            CritDamage = 1,
        };

        // Act
        var result = service.CalculateReceivedDamage( incomingDamage, stoneGiant );

        // Assert
        int expectedMin = ( int )( 100 * 0.85f );
        Assert.Equal( expectedMin, result );
    }

    private static SingleFighter CreateDefaultFighter()
    {
        return new SingleFighter(
            "Test",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
    }
}

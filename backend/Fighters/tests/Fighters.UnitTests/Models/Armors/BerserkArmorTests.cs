using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Armors;

public class BerserkArmorTests
{
    public static TheoryData<int, int, int, int, int> TestData =>
        new TheoryData<int, int, int, int, int>
        {
            { 100,  50, 100,  50, 100 },
            {  50, 100, 200, 150, 300 },
            {   0, 100, 200, 200, 400 }
        };

    [Theory]
    [MemberData( nameof( TestData ) )]
    public void ModifyWeaponDamage_AppliesCorrectMultiplier(
        int remainingHealthPercent,
        int baseMinDamage,
        int baseMaxDamage,
        int expectedMinDamage,
        int expectedMaxDamage )
    {
        // Arrange
        IFighter fighter = CreateBerserk();
        int maxHp = fighter.GetMaxHealth();

        int targetHp = maxHp * remainingHealthPercent / 100;
        int damageToDeal = maxHp - targetHp;
        fighter.TakeDamage( damageToDeal );

        var armor = new BerserkArmor();
        var baseDamage = new DamageStats
        {
            MinDamage = baseMinDamage,
            MaxDamage = baseMaxDamage,
            Type = DamageType.Physic,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( expectedMinDamage, result.MinDamage );
        Assert.Equal( expectedMaxDamage, result.MaxDamage );
    }

    private static IFighter CreateBerserk()
    {
        IFighter fighter = FighterBuilder.CreateWithStats(
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 }
        );
        fighter.SetArmor( new BerserkArmor() );

        return fighter;
    }
}

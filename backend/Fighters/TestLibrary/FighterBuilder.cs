using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace TestLibrary;

public static class FighterBuilder
{
    public static SingleFighter CreateDefault( string name = "TestFighter" )
    {
        return new SingleFighter(
            name,
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
    }
}

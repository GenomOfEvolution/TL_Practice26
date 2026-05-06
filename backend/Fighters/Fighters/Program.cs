using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.GameManager;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;

var randService = new DefaultRandomService();
var damageService = new DamageService( randService );
var gameManager = new GameManager( new BattleLogger(), damageService );

var fighterA = new SingleFighter(
    "FighterA",
    new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
    new HumanRace(),
    new NoSpeciality(),
    new NoArmor(),
    new Fists()
);

var fighterB = new SingleFighter(
    "FighterB",
    new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
    new HumanRace(),
    new NoSpeciality(),
    new NoArmor(),
    new Fists()
);

// Act
var winner = gameManager.Play( fighterA, fighterB );
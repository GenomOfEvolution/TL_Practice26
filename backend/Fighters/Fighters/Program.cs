using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.GameManager;
using Fighters.Models.ItemCatalog;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Services.BattleLogger;
using Fighters.Services.DamageService;
using Fighters.Services.InitiativeService;

var randService = new DefaultRandomService();
var damageService = new DamageService( randService );
var initiativeService = new InitiativeService( randService );
var gameManager = new GameManager( new BattleLogger(), damageService, initiativeService );

IItemCatalog<IWeapon> weapnCatalog = new WeaponCatalog(
[
    new( new Fists(), 0 ),
] );

IItemCatalog<IArmor> armorCatalog = new ArmorCatalog( [
    new (new NoArmor(), 0),
] );

FighterFactory factory = new(
    new RaceFactory(),
    new FighterStatFactory(),
    new WeaponFactory( weapnCatalog ),
    new ArmorFactory( armorCatalog ),
    pointsPerFighter: 20
);

IFighter fighterA = factory.CreateFighter();
IFighter fighterB = factory.CreateFighter();

gameManager.Play( fighterA, fighterB );
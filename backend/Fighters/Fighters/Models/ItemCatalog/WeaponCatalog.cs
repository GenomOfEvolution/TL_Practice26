using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MagicWeapons;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Models.Weapons.RangedWeapons;

namespace Fighters.Models.ItemCatalog;

public class WeaponCatalog : IItemCatalog<IWeapon>
{
    private readonly IReadOnlyList<CatalogEntry<IWeapon>> _entries =
    [
        // Оружия ближнего боя
        new( new Fists(), 0 ),
        new( new WoodenSword(), 5 ),
        new( new Club(), 8 ),
        new( new Dagger(), 8 ),
        new( new GrassBlade(), 15 ),
        new( new HuntingKnife(), 15 ),
        new( new ReinforcedClub(), 15 ),
        new( new TheSeparator(), 25 ),
        new( new CeremonialKnife(), 25 ),
        new( new DragonSlayer(), 32 ),
        new( new MonoBlade(), 40 ),

        // Оружия дальнего боя
        new( new AlchemicalConcoction(), 10 ),
        new( new Brick(), 13 ),
        new( new CrudeBow(), 6 ),
        new( new GhostRifle(), 42 ),
        new( new GoldenDeagle(), 25 ),
        new( new HeavyCrossbow(), 20 ),
        new( new Longbow(), 14 ),
        new( new Revolver(), 10 ),
        new( new RocketLauncher(), 33 ),
        new( new StarCannnon(), 25 ),

        // Магические оружия
        new( new ApprenticeWand(), 5 ),
        new( new DragonBreath(), 30 ),
        new( new LeafWand(), 15 ),
        new( new Nirvana(), 40 ),
        new( new OakStaff(), 7 ),
        new( new SlitherWand(), 20 ),
        new( new SparkWand(), 15 ),
    ];

    public IReadOnlyList<CatalogEntry<IWeapon>> GetAll() => _entries;

    public CatalogEntry<IWeapon> GetByIndex( int index )
    {
        if ( index < 0 || index >= _entries.Count )
        {
            throw new ArgumentOutOfRangeException( nameof( index ), "Индекс выходит за пределы каталога." );
        }

        return _entries[ index ];
    }

    public void PrintAvailable( int maxPoints )
    {
        IReadOnlyList<CatalogEntry<IWeapon>> available = GetAvailable( maxPoints );

        if ( available.Count == 0 )
        {
            Console.WriteLine( "Нет доступного оружия за эти очки." );

            return;
        }

        Console.WriteLine( "Список доступного оружия:" );
        for ( int i = 0; i < available.Count; i++ )
        {
            var entry = available[ i ];
            Console.WriteLine(
                $"""
                [{i}] {entry.Item.Name} — {entry.Price} очк.
                - Описание: {entry.Item.Description}
                - [{entry.Item.Rarity}], [{entry.Item.WeaponType}]
                """
            );
        }
    }

    public IReadOnlyList<CatalogEntry<IWeapon>> GetAvailable( int maxPoints )
    {
        return _entries
            .Where( e => e.Price <= maxPoints )
            .OrderBy( e => e.Item.WeaponType )
            .ThenBy( e => e.Price )
            .ToList()
            .AsReadOnly();
    }
}
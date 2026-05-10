using Fighters.Models.Weapons;

namespace Fighters.Models.ItemCatalog;

public class WeaponCatalog : IItemCatalog<IWeapon>
{
    private readonly IReadOnlyList<CatalogEntry<IWeapon>> _entries;

    public WeaponCatalog( IEnumerable<CatalogEntry<IWeapon>> entries )
    {
        _entries = entries.ToList().AsReadOnly();
    }

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
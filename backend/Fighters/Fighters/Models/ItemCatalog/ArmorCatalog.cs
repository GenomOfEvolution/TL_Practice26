using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.ItemCatalog;

public class ArmorCatalog : IItemCatalog<IArmor>
{
    private readonly IReadOnlyList<CatalogEntry<IArmor>> _entries;

    public ArmorCatalog( IEnumerable<CatalogEntry<IArmor>> entries )
    {
        _entries = entries.ToList().AsReadOnly();
    }

    public IReadOnlyList<CatalogEntry<IArmor>> GetAll() => _entries;

    public CatalogEntry<IArmor> GetByIndex( int index )
    {
        if ( index < 0 || index >= _entries.Count )
        {
            throw new ArgumentOutOfRangeException( nameof( index ), "Индекс выходит за пределы каталога." );
        }

        return _entries[ index ];
    }

    public void PrintAvailable( int maxPoints )
    {
        IReadOnlyList<CatalogEntry<IArmor>> available = GetAvailable( maxPoints );

        if ( available.Count == 0 )
        {
            Console.WriteLine( "Нет доступного оружия за эти очки." );
            return;
        }

        Console.WriteLine( "Список доступной брони:" );
        for ( int i = 0; i < available.Count; i++ )
        {
            CatalogEntry<IArmor> entry = available[ i ];
            Console.WriteLine(
                $"""
                [{i}] {entry.Item.Name} — {entry.Price} очк.
                - Описание: {entry.Item.Description}
                """
            );
        }
    }

    public IReadOnlyList<CatalogEntry<IArmor>> GetAvailable( int maxPoints )
    {
        return _entries
            .Where( e => e.Price <= maxPoints )
            .ToList()
            .AsReadOnly();
    }
}

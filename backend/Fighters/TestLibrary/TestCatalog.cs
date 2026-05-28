using Fighters.Models.Item;
using Fighters.Models.ItemCatalog;

namespace Fighters.TestLibrary;

public class TestCatalog<T>( params CatalogEntry<T>[] entries ) : IItemCatalog<T> where T : IItem
{
    private readonly IReadOnlyList<CatalogEntry<T>> _entries = entries;

    public IReadOnlyList<CatalogEntry<T>> GetAll() => _entries;

    public IReadOnlyList<CatalogEntry<T>> GetAvailable( int maxPoints ) =>
        _entries.Where( e => e.Price <= maxPoints ).ToList().AsReadOnly();

    public void PrintAvailable( int maxPoints ) { }

    public CatalogEntry<T> GetByIndex( int index )
    {
        if ( index < 0 || index >= _entries.Count )
        {
            throw new ArgumentOutOfRangeException( nameof( index ) );
        }

        return _entries[ index ];
    }
}

using Fighters.Models.Item;

namespace Fighters.Models.ItemCatalog;

public interface IItemCatalog<T> where T : IItem
{
    IReadOnlyList<CatalogEntry<T>> GetAll();
    IReadOnlyList<CatalogEntry<T>> GetAvailable( int maxPoints );
    void PrintAvailable( int maxPoints );
    CatalogEntry<T> GetByIndex( int index );
}

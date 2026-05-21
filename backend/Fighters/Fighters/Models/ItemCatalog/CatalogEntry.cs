using Fighters.Models.Item;

namespace Fighters.Models.ItemCatalog;

public record CatalogEntry<T>( T Item, int Price ) where T : IItem;
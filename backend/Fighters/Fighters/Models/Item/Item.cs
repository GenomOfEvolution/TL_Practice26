namespace Fighters.Models.Item;

public interface IItem
{
    string Name { get; }
    string Description { get; }
    ItemRarity Rarity { get; }
}
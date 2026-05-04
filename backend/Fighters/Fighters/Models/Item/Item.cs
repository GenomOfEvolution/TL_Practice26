namespace Fighters.Models.Item;

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }
    ItemRarity Rarity { get; set; }
}
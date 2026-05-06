using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class NoArmor : IArmor
{
    public IReadOnlyDictionary<DamageType, float> Resistances => throw new NotImplementedException();

    public string Name => "Без брони";

    public string Description => "Ваш боец практически голый!";

    public ItemRarity Rarity => ItemRarity.Common;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }
}

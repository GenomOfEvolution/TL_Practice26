using Fighters.Models.Damage;

namespace Fighters.Extensions;

public static class DamageTypeExtensions
{
    public static string GetTypeNameRu( this DamageType type ) => type switch
    {
        DamageType.Physic => "физический",
        DamageType.Magic => "магический",
        DamageType.Pure => "чистый",
        DamageType.Poison => "токсичный",
        _ => type.ToString()
    };
}

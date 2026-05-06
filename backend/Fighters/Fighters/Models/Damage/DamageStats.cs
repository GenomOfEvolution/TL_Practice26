namespace Fighters.Models.Damage;

public class DamageStats
{
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public DamageType Type { get; set; }

    public float CritChance { get; set; }
    public float CritDamage { get; set; }
}
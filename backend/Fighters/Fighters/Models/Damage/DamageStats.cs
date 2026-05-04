namespace Fighters.Models.Damage;

public enum DamageType
{
    Physic,
    Magic,
    Pure,
    Poison,
}

public class DamageStats
{
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public DamageType Type { get; set; }
}

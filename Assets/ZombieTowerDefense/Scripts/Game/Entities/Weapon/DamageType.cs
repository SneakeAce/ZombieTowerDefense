using System;

[Flags]
public enum DamageType
{
    None = 0,
    BaseDamage = 1 << 0,
    ArmorPiercingDamage = 1 << 1,
    ExplosiveDamage = 1 << 2,
}

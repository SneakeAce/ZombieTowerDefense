using System;

[Flags]
public enum EnemyUnitType
{
    None = 0,
    Zombie = 1 << 0,
    StrenghtZombie = 1 << 1,
}

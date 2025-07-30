using System;

[Flags]
public enum PoolType
{
    None = 1 << 0,
    PlayerUnitPool = 1 << 1,
    EnemyUnitPool = 1 << 2,
}

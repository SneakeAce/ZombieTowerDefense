using System;

[Flags]
public enum PoolType
{
    PlayerUnitPool = 1 << 0,
    EnemyUnitPool = 1 << 1,
}

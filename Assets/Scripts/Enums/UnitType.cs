using System;

[Flags]
public enum UnitType
{
    None = 0,
    Marine = 1 << 0,
    Heavygunner = 1 << 1,

    Zombie = 1 << 2,
}

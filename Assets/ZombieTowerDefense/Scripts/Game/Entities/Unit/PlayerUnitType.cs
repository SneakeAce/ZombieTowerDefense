using System;

[Flags]
public enum PlayerUnitType
{
    None = 0,
    Marine = 1 << 0,
    Heavygunner = 1 << 1,
}

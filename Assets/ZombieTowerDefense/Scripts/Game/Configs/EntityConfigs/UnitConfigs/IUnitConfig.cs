using System;

public interface IUnitConfig<TEnum> : IConfig
    where TEnum : Enum
{
    TEnum UnitType { get; }

    UnitMainStats UnitMainStats { get; }
}

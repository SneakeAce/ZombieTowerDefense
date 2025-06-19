using System.Collections.Generic;
using System;

public interface IPoolManager
{
    Dictionary<PoolType, Dictionary<int, IObjectPool>> AvailablePools { get; }

    IObjectPool GetPool<TEnum>(PoolType poolType, TEnum type)
        where TEnum : Enum;
}

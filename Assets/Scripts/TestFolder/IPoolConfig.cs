using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public interface IPoolConfig
{
    int InitialPoolSize { get; }
    bool CanExpandPool { get; }
    PoolType PoolType { get;}
    Transform Container { get; }
    List<UnitConfig> Configs { get; }
}

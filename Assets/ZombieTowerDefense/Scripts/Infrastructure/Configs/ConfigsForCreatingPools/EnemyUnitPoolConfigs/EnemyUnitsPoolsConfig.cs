using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolConfigs/UnitPoolConfigs/EnemyUnitsPoolsConfigs", fileName = "EnemyUnitsPoolsConfigs")]
public class EnemyUnitsPoolsConfig : PoolsConfig<EnemyPoolStats>
{
    [field: SerializeField] public List<EnemyPoolStats> Configs { get; private set; }

    public override List<EnemyPoolStats> PoolConfigs => Configs;

    public override EnemyPoolStats GetPool<T>(T type)
    {
        return Configs.Find(c => EqualityComparer<T>.Default.Equals((T)(object)c.UnitType, type));
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolConfigs/UnitPoolConfigs/PlayerUnitsPoolsConfigs", fileName = "PlayerUnitsPoolsConfigs")]
public class PlayerUnitsPoolsConfig : PoolsConfig<PlayerPoolStats>
{
    [field: SerializeField] public List<PlayerPoolStats> Configs { get; private set; }

    public override List<PlayerPoolStats> PoolConfigs => Configs;

    public override PlayerPoolStats GetPool<T>(T type)
    {
        return Configs.Find(c => EqualityComparer<T>.Default.Equals((T)(object)c.UnitType, type));
    }
}

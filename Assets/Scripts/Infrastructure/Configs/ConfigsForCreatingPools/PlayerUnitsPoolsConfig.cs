using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolConfigs/UnitPoolConfigs/PlayerUnitPoolConfigs")]
public class PlayerUnitsPoolsConfig : PoolsConfig
{
    public override PoolStats GetPool<T>(T type)
    {
        if (type is not UnitType unitType)
            throw new ArgumentException("Invalid arguments. (T)type is not UnitType in PlayerUnitsPoolsConfig.");

        return PoolConfigs.Find(type => type.UnitTypeInPool == unitType);
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigForCreatingPool/PlayerControlledUnitConfigs", fileName = "PlayerControlledUnitConfigs")]
public class PlayerControlledUnitConfigs : ScriptableObject, IPoolConfig
{
    [field: SerializeField] public List<UnitConfig> UnitConfigs { get; private set; }

    public List<UnitConfig> Configs => UnitConfigs;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigForCreatingPool/PlayerControlledUnitConfigs", fileName = "PlayerControlledUnitConfigs")]
public class PlayerControlledUnitConfigs : ScriptableObject, IPoolConfig<PlayerUnitConfig>
{
    [field: SerializeField] public PoolStats<PlayerUnitConfig> PoolCharacteristics { get; private set; }

    public int PoolSize => PoolCharacteristics.PoolSize;
    public bool CanExpand => PoolCharacteristics.CanExpand;
    public PoolType Type => PoolCharacteristics.Type;
    public Transform PoolContainer => PoolCharacteristics.PoolContainer;
    public List<PlayerUnitConfig> Configs => PoolCharacteristics.Configs;
}

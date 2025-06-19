using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigForCreatingPool/PlayerControlledUnitConfigs", fileName = "PlayerControlledUnitConfigs")]
public class PlayerControlledUnitConfigs : ScriptableObject, IPoolConfig<UnitConfig>
{
    [field: SerializeField] public PoolCharacteristics<UnitConfig> PoolCharacteristics { get; private set; }

    public int PoolSize => PoolCharacteristics.PoolSize;
    public bool CanExpand => PoolCharacteristics.CanExpand;
    public PoolType Type => PoolCharacteristics.Type;
    public Transform PoolContainer => PoolCharacteristics.PoolContainer;
    public List<UnitConfig> Configs => PoolCharacteristics.Configs;
}

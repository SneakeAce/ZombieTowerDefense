using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigForCreatingPool/PlayerControlledUnitConfigs", fileName = "PlayerControlledUnitConfigs")]
public class PlayerControlledUnitConfigs : ScriptableObject, IPoolConfig
{
    [field: SerializeField] public int PoolSize { get; private set; }
    [field: SerializeField] public bool CanExpand { get; private set; }
    [field: SerializeField] public PoolType Type { get; private set; }
    [field: SerializeField] public Transform PoolContainer { get; private set; }
    [field: SerializeField] public List<UnitConfig> UnitConfigs { get; private set; }

    public int InitialPoolSize => PoolSize;
    public bool CanExpandPool => CanExpand;
    public PoolType PoolType => Type;
    public Transform Container => PoolContainer;
    public List<UnitConfig> Configs => UnitConfigs;

}

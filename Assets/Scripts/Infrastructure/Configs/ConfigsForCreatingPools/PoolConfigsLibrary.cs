using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/PoolConfigsLibrary")]
public class PoolConfigsLibrary : ScriptableObject, ILibraryConfigs<PoolsConfig>
{
    [field: SerializeField] public List<PoolsConfig> PoolConfigs { get; private set; }

    public List<PoolsConfig> Configs => PoolConfigs;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/PlayerUnitPoolConfigsLibrary")]
public class PlayerUnitPoolConfigsLibrary : ScriptableObject, ILibraryConfigs<PlayerUnitsPoolsConfig>
{
    [field: SerializeField] public List<PlayerUnitsPoolsConfig> Configs { get; private set; }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/EnemyUnitPoolConfigsLibrary")]
public class EnemyUnitPoolConfigsLibrary : ScriptableObject, ILibraryConfigs<EnemyUnitsPoolsConfig>
{
    [field: SerializeField] public List<EnemyUnitsPoolsConfig> Configs { get; private set; }
}

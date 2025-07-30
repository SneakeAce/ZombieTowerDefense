using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/UnitConfigs/EnemyUnitConfig")]
public class EnemyUnitConfig : UnitConfig
{
   [field: SerializeField] public EnemyUnitSpawnStats EnemyUnitSpawnStats { get; private set; }
}

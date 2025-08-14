using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/UnitConfigs/EnemyUnitConfig")]
public class EnemyUnitConfig : UnitConfig, IUnitConfig<EnemyUnitType>
{
    [field: SerializeField] public EnemyUnitType Type { get; private set; }
    [field: SerializeField] public EnemyUnitSpawnStats EnemyUnitSpawnStats { get; private set; }

    public EnemyUnitType UnitType => Type;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SystemConfigs/SpawnerConfigs/EnemySpawnerConfig",
    fileName = "EnemySpawnerConfig")]
public class EnemyUnitSpawnerControllerConfig : ScriptableObject, IConfig
{
    [field: SerializeField] public List<EnemySpawnerStats> ListEnemiesInSpawner { get; private set; }
    [field: SerializeField] public float TimeBetweenStartSpawn { get; private set; }
}

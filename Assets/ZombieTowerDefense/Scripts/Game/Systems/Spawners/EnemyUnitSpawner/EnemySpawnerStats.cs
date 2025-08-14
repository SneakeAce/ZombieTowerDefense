using System;
using UnityEngine;

[Serializable]
public class EnemySpawnerStats 
{
    [field: SerializeField] public EnemyUnitType EnemyUnitType { get; private set; }
    [field: SerializeField] public int SpawnCountPerOnce { get; private set; }
    [field: SerializeField] public int MaxCountEnemyByType { get; private set; }
    [field: SerializeField] public float TimeBetweenSpawn { get; private set; }
}

using System;
using UnityEngine;

[Serializable]
public class EnemySpawnerStats 
{
    [field: SerializeField] public UnitType EnemyUnitType { get; private set; }
    [field: SerializeField] public int SpawnCountPerOnce { get; private set; }
    [field: SerializeField] public int MaxCountEnemyByType { get; private set; }
    [field: SerializeField] public float TimeBetweenSpawn { get; private set; }
}

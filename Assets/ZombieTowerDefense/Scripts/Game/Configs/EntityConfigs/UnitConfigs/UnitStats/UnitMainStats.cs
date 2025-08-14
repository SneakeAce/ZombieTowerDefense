using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class UnitMainStats
{
    [field: SerializeField] public AssetReference Prefab { get; private set; }
    [field: SerializeField] public MoveStats MoveStats { get; private set; }
    [field: SerializeField] public HealthStats HealthStats { get; private set; }
    [field: SerializeField] public AttackStats AttackStats { get; private set; }
}

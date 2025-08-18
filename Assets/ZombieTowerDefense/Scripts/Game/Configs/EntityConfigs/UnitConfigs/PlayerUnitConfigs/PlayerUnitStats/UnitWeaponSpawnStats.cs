using System;
using UnityEngine;

[Serializable]
public class UnitWeaponSpawnStats
{
    [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
    [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion SpawnRotation { get; private set; }

}

using System;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    [field: SerializeField] public GameObject SpawnRayPoint { get; private set; }
    [field: SerializeField] public int BaseWeaponDamage { get; private set; }
    [field: SerializeField] public float RadiusAttack { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float BaseDelayBetweenAttack { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer {  get; private set; }
    [field: SerializeField] public DamageType Type { get; private set; }
}

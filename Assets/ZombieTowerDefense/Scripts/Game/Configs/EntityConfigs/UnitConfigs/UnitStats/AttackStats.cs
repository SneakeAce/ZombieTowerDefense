using System;
using UnityEngine;

[Serializable]
public class AttackStats
{
    [field: SerializeField] public float BaseDelayBetweenAttack { get; private set; }
    [field: SerializeField] public float BaseValueDamage { get; private set; }
}

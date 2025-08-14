using System;
using UnityEngine;

[Serializable]
public class UnitHiringStats
{
    [field: SerializeField] public int HiringCost { get; private set; }
    [field: SerializeField] public float HiringTime { get; private set; }
}

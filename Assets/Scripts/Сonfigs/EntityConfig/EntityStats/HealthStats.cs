using System;
using UnityEngine;

[Serializable]
public class HealthStats
{
    [field: SerializeField] public float BaseValueHealth { get; private set; }
    [field: SerializeField] public float BaseValueRegenerationHealth { get; private set; }  
    [field: SerializeField] public float BaseDelayRegenerationHealth { get; private set; }
}

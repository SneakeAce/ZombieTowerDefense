using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolCharacteristics<T> 
    where T : ScriptableObject
{
    [field: SerializeField] public int PoolSize { get; private set; }
    [field: SerializeField] public bool CanExpand { get; private set; }
    [field: SerializeField] public PoolType Type { get; private set; }
    [field: SerializeField] public Transform PoolContainer { get; private set; }
    [field: SerializeField] public List<T> Configs { get; private set; }
}

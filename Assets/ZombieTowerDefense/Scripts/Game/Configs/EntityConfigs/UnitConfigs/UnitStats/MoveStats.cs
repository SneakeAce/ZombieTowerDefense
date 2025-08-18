using System;
using UnityEngine;

[Serializable]
public class MoveStats
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
}

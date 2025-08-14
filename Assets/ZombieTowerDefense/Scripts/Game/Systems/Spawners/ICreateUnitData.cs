using System;
using UnityEngine;

public interface ICreateUnitData
{
    public Vector3 PositionToMove { get; }
    public Vector3 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
    public float HiringTime { get; }
    public float HiringCost { get; }
    public Enum UnitTypeEnum { get; }
}

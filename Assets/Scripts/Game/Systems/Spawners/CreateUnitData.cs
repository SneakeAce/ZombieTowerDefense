using UnityEngine;

public struct CreateUnitData
{
    public CreateUnitData(UnitType unitType, 
        Vector3 positionToMove, Vector3 spawnPosition, Quaternion spawnRotation,
        float hiringtime, float hiringCost)
    {
        UnitType = unitType;
        PositionToMove = positionToMove;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
        HiringTime = hiringtime;
        HiringCost = hiringCost;
    }

    public UnitType UnitType { get; }
    public Vector3 PositionToMove { get; }
    public Vector3 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
    public float HiringTime { get; }
    public float HiringCost { get; }
}

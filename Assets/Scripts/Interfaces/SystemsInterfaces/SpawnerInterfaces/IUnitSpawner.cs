using UnityEngine;

public interface IUnitSpawner
{
    void CreateUnit(UnitType unitType, Vector3 positionToMove);
}

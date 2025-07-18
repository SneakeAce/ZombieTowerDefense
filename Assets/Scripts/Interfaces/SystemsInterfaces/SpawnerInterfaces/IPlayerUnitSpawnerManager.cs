using UnityEngine;

public interface IPlayerUnitSpawnerManager
{
    void OnTrySpawn(UnitType unitType, Vector3 positionToMove);
}

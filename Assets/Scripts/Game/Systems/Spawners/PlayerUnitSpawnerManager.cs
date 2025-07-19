using UnityEngine;

public class PlayerUnitSpawnerManager : IPlayerUnitSpawnerManager
{
    private IPlayerUnitSpawner _unitSpawner;

    public PlayerUnitSpawnerManager(IPlayerUnitSpawner unitSpawner)
    {
        _unitSpawner = unitSpawner;
    }

    public void OnTrySpawn(UnitType unitType, Vector3 positionToMove)
    {
        _unitSpawner.CreateUnit(unitType, positionToMove);
    }
}

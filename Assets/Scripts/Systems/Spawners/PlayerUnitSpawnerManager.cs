public class PlayerUnitSpawnerManager : IPlayerUnitSpawnerManager
{
    private IPlayerUnitSpawner _unitSpawner;

    public PlayerUnitSpawnerManager(IPlayerUnitSpawner unitSpawner)
    {
        _unitSpawner = unitSpawner;
    }

    public void OnTrySpawn(UnitType unitType)
    {
        _unitSpawner.CreateUnit(unitType);
    }
}

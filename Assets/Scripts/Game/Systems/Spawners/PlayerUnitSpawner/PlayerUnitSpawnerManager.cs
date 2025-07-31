public class PlayerUnitSpawnerManager : IPlayerUnitSpawnerManager
{
    private IPlayerUnitSpawner _unitSpawner;

    public PlayerUnitSpawnerManager(IPlayerUnitSpawner unitSpawner)
    {
        _unitSpawner = unitSpawner;
    }

    public void OnTrySpawn(CreateUnitData createUnitData)
    {
        _unitSpawner.CreateUnit(createUnitData);
    }
}

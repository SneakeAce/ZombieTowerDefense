using System;
using UnityEngine;

public class UnitHiringController : IUnitHiringController
{
    private IPlayerUnitSpawnerManager _playerUnitSpawnerManager;
    private IUnitHiringButtonsController _unitHiringButtonsController;
    private IConfigsProvider _configsProvider;

    private Func<Vector3> _setPositionToMove;
    private Func<Vector3> _setSpawnPosition;

    private ILibraryConfigs<PlayerUnitConfig> _playerUnitConfigs;

    public UnitHiringController(IPlayerUnitSpawnerManager playerUnitSpawnerManager, 
        IUnitHiringButtonsController unitHiringButtonsController, IConfigsProvider configsProvider)
    {
        _playerUnitSpawnerManager = playerUnitSpawnerManager;
        _unitHiringButtonsController = unitHiringButtonsController;
        _configsProvider = configsProvider;
    }

    public IUnitHiringButtonsController UnitHiringButtonsController => _unitHiringButtonsController;

    public void Initialize()
    {
        _playerUnitConfigs = _configsProvider.GetConfigsLibrary<PlayerUnitConfig>();

        for (int i = 0; i < _unitHiringButtonsController.HiringButtons.Count; i++)
        {
            IUnitHiringButton button = _unitHiringButtonsController.HiringButtons[i];

            if (button == null)
                continue;

            button.OnHireRequested += OnHireRequested;
        }
    }

    public void SetPositionToMoveProvider(Func<Vector3> getPosition)
    {
        _setPositionToMove = getPosition;
    }    
    
    public void SetSpawnPositionProvider(Func<Vector3> getPosition)
    {
        _setSpawnPosition = getPosition;
    }

    private void OnHireRequested(UnitType unitType)
    {
        PlayerUnitConfig config = null;

        foreach (PlayerUnitConfig unitConfig in _playerUnitConfigs.Configs)
        {
            if (unitConfig.UnitMainStats.UnitType == unitType)
            {
                config = unitConfig;
                break;
            }
            else
            {
                throw new NullReferenceException($"[UnitHiringController] - config is null!");
            }
        }

        Vector3 positionToMove = _setPositionToMove.Invoke();
        Vector3 spawnPosition = _setSpawnPosition.Invoke();

        CreateUnitData createUnitData = new CreateUnitData(config.UnitMainStats.UnitType,
            positionToMove, spawnPosition, Quaternion.identity,
            config.UnitMainStats.HireUnitStats.HiringTime, config.UnitMainStats.HireUnitStats.HiringCost);

        _playerUnitSpawnerManager.OnTrySpawn(createUnitData);
    }
}

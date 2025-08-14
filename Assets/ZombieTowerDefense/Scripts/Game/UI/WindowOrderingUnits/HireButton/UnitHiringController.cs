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

    private void OnHireRequested(PlayerUnitType unitType)
    {
        PlayerUnitConfig config = null;

        foreach (PlayerUnitConfig unitConfig in _playerUnitConfigs.Configs)
        {
            if (unitConfig.UnitType == unitType)
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

        CreateUnitData<PlayerUnitType> createUnitData = new CreateUnitData<PlayerUnitType>(config.UnitType,
            positionToMove, spawnPosition, Quaternion.identity,
            config.UnitHiringStats.HiringTime, config.UnitHiringStats.HiringCost);

        _playerUnitSpawnerManager.OnTrySpawn(createUnitData);
    }
}

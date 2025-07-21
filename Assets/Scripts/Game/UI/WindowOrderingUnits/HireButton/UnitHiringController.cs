using System;
using UnityEngine;

public class UnitHiringController : IUnitHiringController
{
    private IPlayerUnitSpawnerManager _playerUnitSpawnerManager;
    private IUnitHiringButtonsController _unitHiringButtonsController;

    private Func<Vector3> _setSpawnPosition;

    public UnitHiringController(IPlayerUnitSpawnerManager playerUnitSpawnerManager, 
        IUnitHiringButtonsController unitHiringButtonsController)
    {
        _playerUnitSpawnerManager = playerUnitSpawnerManager;
        _unitHiringButtonsController = unitHiringButtonsController;
    }

    public IUnitHiringButtonsController UnitHiringButtonsController => _unitHiringButtonsController;

    public void Initialize()
    {
        for (int i = 0; i < _unitHiringButtonsController.HiringButtons.Count; i++)
        {
            IUnitHiringButton button = _unitHiringButtonsController.HiringButtons[i];

            if (button == null)
                continue;

            button.OnHireRequested += OnHireRequested;
        }
    }

    public void SetSpawnPositionProvider(Func<Vector3> getPosition)
    {
        _setSpawnPosition = getPosition;
    }

    private void OnHireRequested(UnitType unitType)
    {
        Vector3 spawnPosition = _setSpawnPosition.Invoke();

        _playerUnitSpawnerManager.OnTrySpawn(unitType, spawnPosition);
    }
}

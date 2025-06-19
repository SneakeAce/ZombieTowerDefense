using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerManager
{
    private PlayerInput _playerInput;
    private IPlayerUnitSpawner _unitSpawner;

    private UnitType _unitType = UnitType.Marine;

    public SpawnerManager(PlayerInput playerInput, IPlayerUnitSpawner unitSpawner)
    {
        _playerInput = playerInput;
        _unitSpawner = unitSpawner;

        _playerInput.UISelectUnit.MarineUnitCreate.performed += MarineCreating;
    }

    private void Spawn()
    {
        _unitSpawner.CreateUnit(_unitType);
    }

    private void MarineCreating(InputAction.CallbackContext context)
    {
        Spawn();
        Debug.Log("MarineCreating create Unit");
    }
}

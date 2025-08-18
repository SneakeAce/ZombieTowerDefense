using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitMoveTargetSelector : IDisposable, IInitialize
{
    private PlayerInput _playerInput;
    private LayerMask _groundLayer = 1 << 7;
    private ICommandInvoker _commandInvoker;
    private IPlayerUnit _unit;

    public UnitMoveTargetSelector(PlayerInput playerInput, ICommandInvoker commandInvoker)
    {   
        _playerInput = playerInput;
        _commandInvoker = commandInvoker;
    }

    public void Initialize()
    {
        _playerInput.SelectionUnit.ChoosePoint.performed += OnChooseMoveTarget;
    }

    public void Dispose()
    {
        _playerInput.SelectionUnit.ChoosePoint.performed -= OnChooseMoveTarget;

    }

    public void SetUnit(IPlayerUnit unit) => _unit = unit;
    
    private void OnChooseMoveTarget(InputAction.CallbackContext context)
    {
        Debug.Log("OnChooseMoveTarget");

        if (_unit == null || _unit.IsSelected == false) return;

        Vector2 mousePosition = _playerInput.SelectionUnit.MousePosition.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _groundLayer))
        {
            _commandInvoker.AddCommand(new MoveCommand(_unit, hitInfo.point));

            _commandInvoker.ExecuteCommand();
        }

    }

}

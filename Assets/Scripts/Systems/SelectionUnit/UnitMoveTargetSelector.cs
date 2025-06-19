using UnityEngine;
using UnityEngine.InputSystem;

public class UnitMoveTargetSelector
{
    private PlayerInput _playerInput;
    private LayerMask _groundLayer = 1 << 7;
    private ICommandInvoker _commandInvoker;
    private IUnit _unit;

    public UnitMoveTargetSelector(PlayerInput playerInput, ICommandInvoker commandInvoker)
    {   
        _playerInput = playerInput;
        _commandInvoker = commandInvoker;

        _playerInput.SelectionUnit.ChoosePoint.performed += OnChooseMoveTarget;
    }

    public void SetUnit(IUnit unit) => _unit = unit;
    
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

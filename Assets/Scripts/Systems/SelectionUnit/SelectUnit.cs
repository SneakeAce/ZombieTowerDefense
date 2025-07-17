using UnityEngine;
using UnityEngine.InputSystem;

public class SelectUnit
{
    private PlayerInput _playerInput;

    private UnitMoveTargetSelector _unitMoveTargetSelector;

    private LayerMask _unitLayer = 1 << 6;

    public SelectUnit(PlayerInput playerInput, UnitMoveTargetSelector unitMoveTargetSelector)
    {
        _playerInput = playerInput;
        _playerInput.Enable();

        _unitMoveTargetSelector = unitMoveTargetSelector;

        _playerInput.SelectionUnit.SelectUnit.performed += OnSelectionUnit;
    }

    private void OnSelectionUnit(InputAction.CallbackContext context)
    {
        Vector2 inputMousePosition = _playerInput.SelectionUnit.MousePosition.ReadValue<Vector2>();
        Vector3 mousePosition = new Vector3(inputMousePosition.x, inputMousePosition.y, 0f);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _unitLayer))
        {
            if (hitInfo.collider.TryGetComponent(out IUnit unit))
            {
                unit.IsSelected = true;

                _unitMoveTargetSelector.SetUnit(unit);

                Debug.Log("Unit Selected!");
            }
            else
            {
                Debug.Log("No unit selected.");
            }
        }
    }
}

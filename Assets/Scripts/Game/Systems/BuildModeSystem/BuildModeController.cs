using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildModeController : IBuildModeController, IDisposable
{
    // Temporary spawn position untill the Headquarters is ready.
    private Vector3 _tempSpawnPositionUnit = new Vector3(3.1f, 0f, 0f); 

    private PlayerInput _playerInput;

    private IGridCell _currentCell;

    private IGridManager _gridManager;
    private IWindowUnitsHiringController _windowUnitsHiringController;
    private IUnitHiringController _unitHiringController;

    private BuildModeInputHandler _buildModeInputHandler;

    private Vector3 _currentCellPosition;

    private bool _isActiveBuildMode;
    private bool _isOpenWindowUnitHiring;

    public BuildModeController(PlayerInput playerInput, IGridManager gridManager, 
        IWindowUnitsHiringController windowHiringUnitsController, IUnitHiringController unitHiringController,
        BuildModeInputHandler buildModeInputHandler)
    {
        _playerInput = playerInput;

        _gridManager = gridManager;
        _windowUnitsHiringController = windowHiringUnitsController;
        _unitHiringController = unitHiringController;

        _buildModeInputHandler = buildModeInputHandler;
    }

    public void Initialize()
    {
        _playerInput.BuildMode.Enable();

        _buildModeInputHandler.Initialize();

        SubscribeInputEvents();
    }

    public void Dispose()
    {
        UnsubscribeInputEvents();

        UnsubscribeButtonEvents();
    }

    private void SubscribeInputEvents()
    {
        _playerInput.BuildMode.ActivateBuildMode.performed += OnActivateBuildMode;

        _playerInput.BuildMode.MousePosition.performed += OnCursorMoved;
        _playerInput.BuildMode.SelectCell.performed += OnSelectCell;
        _playerInput.BuildMode.DeselectCell.performed += OnDeselectCell;
    }    

    private void UnsubscribeInputEvents()
    {
        _playerInput.BuildMode.ActivateBuildMode.performed -= OnActivateBuildMode;

        _playerInput.BuildMode.MousePosition.performed -= OnCursorMoved;
        _playerInput.BuildMode.SelectCell.performed -= OnSelectCell;
        _playerInput.BuildMode.DeselectCell.performed -= OnDeselectCell;
    }

    private void SubscribeButtonEvents()
    {
        for (int i = 0; i < _unitHiringController.UnitHiringButtonsController.HiringButtons.Count; i++)
        {
            IUnitHiringButton button = _unitHiringController.UnitHiringButtonsController.HiringButtons[i];

            if (button == null)
                continue;

            _unitHiringController.SetPositionToMoveProvider(OnGetCellPosition);
            _unitHiringController.SetSpawnPositionProvider(OnReturnTempSpawnPosition);

            button.ButtonWasPressed += ResetSelectedCell;
        }
    }

    private void UnsubscribeButtonEvents()
    {
        for (int i = 0; i < _unitHiringController.UnitHiringButtonsController.HiringButtons.Count; i++)
        {
            IUnitHiringButton button = _unitHiringController.UnitHiringButtonsController.HiringButtons[i];

            if (button == null)
                continue;

            _unitHiringController.SetPositionToMoveProvider(null);
            _unitHiringController.SetSpawnPositionProvider(null);

            button.ButtonWasPressed -= ResetSelectedCell;
        }
    }

    private void OnActivateBuildMode(InputAction.CallbackContext context)
    {
        _isActiveBuildMode = !_isActiveBuildMode;

        if (_isActiveBuildMode)
        {
            _playerInput.SelectionUnit.Disable();
        }
        else
        {
            ToggleWindowUnitHiring(_isActiveBuildMode);

            _playerInput.SelectionUnit.Enable();
        }
        
        _gridManager.ToggleGridActivity(_isActiveBuildMode);
    }

    private void OnCursorMoved(InputAction.CallbackContext context)
    {
        _buildModeInputHandler.CursorMoved(context);
    }

    private void OnSelectCell(InputAction.CallbackContext context)
    {
        _currentCell = _buildModeInputHandler.SelectCell();

        if (_currentCell == null)
            return;

        _currentCellPosition = _currentCell.GameObject.transform.position;

        ToggleWindowUnitHiring(_isActiveBuildMode);

        SubscribeButtonEvents();
    }

    private Vector3 OnGetCellPosition()
    {
        return _currentCellPosition;
    }

    private Vector3 OnReturnTempSpawnPosition()
    {
        return _tempSpawnPositionUnit;
    }

    private void OnDeselectCell(InputAction.CallbackContext context)
    {
        ResetSelectedCell();
    }

    private void ResetSelectedCell()
    {
        _buildModeInputHandler.ResetCurrentCell();

        ToggleWindowUnitHiring(false);

        _currentCell = null;

        UnsubscribeButtonEvents();
    }

    private void ToggleWindowUnitHiring(bool canOpen)
    {
        _isOpenWindowUnitHiring = canOpen;

        _windowUnitsHiringController.ToggleWindowUnitsHiring(_isOpenWindowUnitHiring);
    }

}

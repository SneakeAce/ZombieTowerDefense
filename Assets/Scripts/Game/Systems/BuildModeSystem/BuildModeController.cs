using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildModeController : IBuildModeController, IDisposable, IInitialize
{
    private PlayerInput _playerInput;

    private IGridCell _currentCell;

    private IGridManager _gridManager;
    private IWindowHiringUnitsController _windowHiringUnitsController;
    private IHiringUnitButtonsController _buttonsController;
    private BuildModeInputHandler _buildModeInputHandler;

    private bool _isActiveBuildMode;

    public BuildModeController(PlayerInput playerInput, IGridManager gridManager, 
        IWindowHiringUnitsController windowHiringUnitsController, IHiringUnitButtonsController buttonsController,
        BuildModeInputHandler buildModeInputHandler)
    {
        _playerInput = playerInput;

        _gridManager = gridManager;
        _windowHiringUnitsController = windowHiringUnitsController;
        _buttonsController = buttonsController;

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
        for (int i = 0; i < _buttonsController.HiringButtons.Count; i++)
        {
            IHiringUnitButton button = _buttonsController.HiringButtons[i];

            if (button == null)
                continue;

            button.SetPosition(_currentCell.GameObject.transform.position);
            button.ButtonWasPressed += ExitBuildMode;
        }
    }

    private void UnsubscribeButtonEvents()
    {
        for (int i = 0; i < _buttonsController.HiringButtons.Count; i++)
        {
            IHiringUnitButton button = _buttonsController.HiringButtons[i];

            if (button == null)
                continue;

            button.SetPosition(_currentCell.GameObject.transform.position);
            button.ButtonWasPressed += ExitBuildMode;
        }
    }

    private void OnActivateBuildMode(InputAction.CallbackContext context)
    {
        _isActiveBuildMode = !_isActiveBuildMode;

        if (_isActiveBuildMode)
            _playerInput.SelectionUnit.Disable();
        else
            _playerInput.SelectionUnit.Enable();
        

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

        Debug.Log($"_currentCell in BuildModeController = {_currentCell}");

        _windowHiringUnitsController.ToggleWindowHiringUnits(_isActiveBuildMode);

        SubscribeButtonEvents();
    }

    private void OnDeselectCell(InputAction.CallbackContext context)
    {
        ExitBuildMode();
    }

    private void ExitBuildMode()
    {
        _buildModeInputHandler.ResetCurrentCell();

        _windowHiringUnitsController.ToggleWindowHiringUnits(_isActiveBuildMode);

        _currentCell = null;

        UnsubscribeButtonEvents();
    }

}

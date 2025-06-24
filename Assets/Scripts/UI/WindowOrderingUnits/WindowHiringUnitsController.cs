using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class WindowHiringUnitsController : IWindowHiringUnitsController, IDisposable
{
    private PlayerInput _playerInput;

    private ICameraManager _cameraManager;
    private IWindowHiringUnitsManager _windowHiringUnitsManager;

    private bool _windowIsOpen;

    public WindowHiringUnitsController(PlayerInput playerInput, ICameraManager cameraManager, 
        IWindowHiringUnitsManager windowHiringUnitsManager)
    {
        Debug.Log("WindowHiringUnitsController Construct called");

        _playerInput = playerInput;
        _playerInput.CallWindowHiringUnits.Enable();

        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;

        _playerInput.CallWindowHiringUnits.ToggleWindowHiringUnits.performed += OnToggleWindowHiringUnits;
    }

    public void Initialization()
    {
        _windowHiringUnitsManager.WindowOrderingCanvas.worldCamera = _cameraManager.MainCamera;

        if (_windowHiringUnitsManager.WindowOrderingCanvas.worldCamera == null)
            throw new NullReferenceException("WorldCamera on WindowOrderingUnitsCanvas is Null!");

        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(false);

    }

    private void OnToggleWindowHiringUnits(InputAction.CallbackContext context) 
    {
        _windowIsOpen = !_windowIsOpen;

        if (_windowIsOpen)
        {
            _playerInput.HireUnits.Enable();
        }
        else
        {
            _playerInput.HireUnits.Disable();
        }

        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(_windowIsOpen);
    }

    public void Dispose()
    {
        _playerInput.CallWindowHiringUnits.ToggleWindowHiringUnits.performed -= OnToggleWindowHiringUnits;
    }
}

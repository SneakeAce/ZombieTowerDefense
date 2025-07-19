using System;
using UnityEngine;

public class WindowHiringUnitsController : IWindowHiringUnitsController, IInitialize
{
    private ICameraManager _cameraManager;
    private IWindowHiringUnitsManager _windowHiringUnitsManager;

    private bool _windowIsOpen;

    public WindowHiringUnitsController(ICameraManager cameraManager, 
        IWindowHiringUnitsManager windowHiringUnitsManager)
    {
        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;
    }

    public void Initialize()
    {
        _windowHiringUnitsManager.WindowOrderingCanvas.worldCamera = _cameraManager.MainCamera;

        if (_windowHiringUnitsManager.WindowOrderingCanvas.worldCamera == null)
            throw new NullReferenceException("WorldCamera on WindowOrderingUnitsCanvas is Null!");

        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(false);
    }

    public void ToggleWindowHiringUnits(bool canBeOpened) 
    {
        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(canBeOpened);
    }

}

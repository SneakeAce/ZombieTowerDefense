using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FirstLevelSceneBootstrapper : IInitializable
{
    private ICameraManager _cameraManager;
    private IWindowHiringUnitsManager _windowHiringUnitsManager;

    private IHiringUnitButtonsController _buttonsController;
    private IWindowHiringUnitsController _windowHiringUnitsController;
    private IBuildModeController _buildModeController;
    private IInitializer _initializer;

    private List<IInitialize> _initializeList = new List<IInitialize>();

    public FirstLevelSceneBootstrapper(ICameraManager cameraManager, 
        IWindowHiringUnitsManager windowHiringUnitsManager, IHiringUnitButtonsController buttonsController,
        IWindowHiringUnitsController windowHiringUnitsController, IBuildModeController buildModeController, 
        IInitializer initializer)
    {
        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;
        _buttonsController = buttonsController;
        _windowHiringUnitsController = windowHiringUnitsController;
        _buildModeController = buildModeController;
        _initializer = initializer;

        FillInitializeList();
    }

    public void Initialize()
    {
        LoadManagersAsync().Forget();
    }

    private void FillInitializeList()
    {
        _initializeList.Add(_buildModeController);
        _initializeList.Add(_windowHiringUnitsController);
        _initializeList.Add(_buttonsController);
    }

    private async UniTask LoadManagersAsync()
    {
        await _cameraManager.LoadAndCreateCameraAsync();
        await _windowHiringUnitsManager.LoadPrefabAsync();

        _initializer.Initialize(_initializeList);

        Debug.Log("FirstLevelSceneBootstrapper LoadManagersAsync end");
    }
}

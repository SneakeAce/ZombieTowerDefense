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

    public FirstLevelSceneBootstrapper(ICameraManager cameraManager, 
        IWindowHiringUnitsManager windowHiringUnitsManager, IHiringUnitButtonsController buttonsController,
        IWindowHiringUnitsController windowHiringUnitsController, IBuildModeController buildModeController)
    {
        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;
        _buttonsController = buttonsController;
        _windowHiringUnitsController = windowHiringUnitsController;
        _buildModeController = buildModeController;
    }

    public void Initialize()
    {
        LoadManagersAsync().Forget();
    }

    private async UniTask LoadManagersAsync()
    {
        await _cameraManager.LoadAndCreateCameraAsync();
        await _windowHiringUnitsManager.LoadPrefabAsync();

        _windowHiringUnitsController.Initialize();
        _buttonsController.Initialize();
        _buildModeController.Initialize();

        Debug.Log("FirstLevelSceneBootstrapper LoadManagersAsync end");
    }
}

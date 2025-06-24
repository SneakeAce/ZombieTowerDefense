using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FirstLevelSceneBootstrapper : IInitializable
{
    private ICameraManager _cameraManager;
    private IWindowHiringUnitsManager _windowHiringUnitsManager;

    private IHiringUnitButtonsController _buttonsController;
    private IWindowHiringUnitsController _windowHiringUnitsController;
    public FirstLevelSceneBootstrapper(ICameraManager cameraManager, 
        IWindowHiringUnitsManager windowHiringUnitsManager, IHiringUnitButtonsController buttonsController,
        IWindowHiringUnitsController windowHiringUnitsController)
    {
        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;
        _buttonsController = buttonsController;
        _windowHiringUnitsController = windowHiringUnitsController;
    }

    public void Initialize()
    {
        Debug.Log("FirstLevelSceneBootstrapper Initialize");

        LoadManagersAsync().Forget();
    }

    private async UniTask LoadManagersAsync()
    {
        await _cameraManager.LoadAndCreateCameraAsync();
        await _windowHiringUnitsManager.LoadPrefabAsync();

        _windowHiringUnitsController.Initialization();
        _buttonsController.Initialization();

        Debug.Log("FirstLevelSceneBootstrapper LoadManagersAsync end");

    }
}

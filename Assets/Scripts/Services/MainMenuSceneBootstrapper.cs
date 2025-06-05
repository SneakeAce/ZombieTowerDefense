using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;

public class MainMenuSceneBootstrapper : IInitializable 
{
    private MainMenuManager _mainMenuManager;
    private CameraManager _cameraManager;
    private MainMenuControllerBinder _mainMenuControllerBinder;

    public MainMenuSceneBootstrapper(MainMenuManager mainMenuManager, CameraManager cameraManager,
        MainMenuControllerBinder mainMenuControllerBinder)
    {
        _mainMenuManager = mainMenuManager;
        _cameraManager = cameraManager;
        _mainMenuControllerBinder = mainMenuControllerBinder;
    }

    public void Initialize()
    {
        Debug.Log("MainMenuBootstrapper Initialize");

        LoadManagersAsync().Forget();
    }

    private async UniTask LoadManagersAsync()
    {
        await _cameraManager.LoadAndCreateCameraAsync();

        await _mainMenuManager.LoadMainMenuPrefabAsync();

        _mainMenuControllerBinder.BindMainMenuController();
        Debug.Log("MainMenuBootstrapper LoadManagersAsync end");

    }
}

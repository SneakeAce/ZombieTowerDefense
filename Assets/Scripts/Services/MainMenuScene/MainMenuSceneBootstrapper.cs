using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;

public class MainMenuSceneBootstrapper : IInitializable 
{
    private IMainMenuManager _mainMenuManager;
    private ICameraManager _cameraManager;
    private MainMenuControllerBinder _mainMenuControllerBinder;

    public MainMenuSceneBootstrapper(IMainMenuManager mainMenuManager, ICameraManager cameraManager,
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

        await _mainMenuManager.LoadPrefabAsync();

        _mainMenuControllerBinder.BindMainMenuController();
        Debug.Log("MainMenuBootstrapper LoadManagersAsync end");
    }
}

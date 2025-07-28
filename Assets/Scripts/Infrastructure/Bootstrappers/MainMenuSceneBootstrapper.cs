using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuSceneBootstrapper : IInitialize
{
    private IMainMenuManager _mainMenuManager;
    private ICameraManager _cameraManager;
    private IMainMenuController _mainMenuController;
    private IInitializer _initializer;

    public MainMenuSceneBootstrapper(IMainMenuManager mainMenuManager, ICameraManager cameraManager, 
        IMainMenuController mainMenuController, IInitializer initializer)
    {
        _mainMenuManager = mainMenuManager;
        _cameraManager = cameraManager;
        _mainMenuController = mainMenuController;
        _initializer = initializer;
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

        _initializer.Initialize(_mainMenuController);

        Debug.Log("MainMenuBootstrapper LoadManagersAsync end");
    }
}

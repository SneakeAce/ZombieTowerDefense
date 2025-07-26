using UnityEngine;

public class MainMenuController : IMainMenuController, IInitialize
{
    private IMainMenuManager _mainMenuManager;
    private ICameraManager _cameraManager;
    private ISceneLoader _sceneLoader;
    private IConfigsProvider _configsProvider;

    private MainMenuSceneConfig _config;

    public MainMenuController(IMainMenuManager mainMenuManager, ICameraManager cameraManager,
        IConfigsProvider configsProvider, ISceneLoader sceneLoader)
    {
        UnityEngine.Debug.Log("MainMenuController");

        _mainMenuManager = mainMenuManager;
        _cameraManager = cameraManager;
        _configsProvider = configsProvider;
        _sceneLoader = sceneLoader;
    }

    public void Initialize()
    {
        GetConfig();

        _mainMenuManager.MainMenuCanvas.worldCamera = _cameraManager.MainCamera;

        _mainMenuManager.MainMenuView.StartLevelButton.onClick.AddListener(OnClickStartLevelButton);
    }

    private void GetConfig() 
    {
        var sceneConfig = _configsProvider.GetSceneConfig();

        if (sceneConfig is MainMenuSceneConfig config)
            _config = config;
        else
            Debug.LogError("[MainMenuController] GetConfig - sceneConfig is not MainMenuConfig.");
    }


    private void OnClickStartLevelButton()
    {
        UnityEngine.Debug.Log("MainMenuController / OnClickStartLevelButton");
        _sceneLoader.LoadSceneAsync(_config.FirstLevelSceneReference).Forget();
    }
}

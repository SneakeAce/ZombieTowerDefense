using System;
using UnityEngine;

public class MainMenuController
{
    private IMainMenuManager _mainMenuManager;
    private ICameraManager _cameraManager;
    private ISceneLoader _sceneLoader;

    private MainMenuSceneConfig _mainMenuSceneConfig;

    public MainMenuController(IMainMenuManager mainMenuManager, ICameraManager cameraManager, 
        MainMenuSceneConfig mainMenuSceneConfig, ISceneLoader sceneLoader)
    {
        UnityEngine.Debug.Log("MainMenuController");

        _mainMenuManager = mainMenuManager;
        _cameraManager = cameraManager;
        _mainMenuSceneConfig = mainMenuSceneConfig;
        _sceneLoader = sceneLoader;

        Initialize();
    }

    private void Initialize()
    {
        UnityEngine.Debug.Log("MainMenuController / Initialize");

        _mainMenuManager.MainMenuCanvas.worldCamera = _cameraManager.MainCamera;
        UnityEngine.Debug.Log($"MainMenuController /  _mainMenuCanvas.worldCamera = {_mainMenuManager.MainMenuCanvas.worldCamera}");

        _mainMenuManager.MainMenuView.StartLevelButton.onClick.AddListener(OnClickStartLevelButton);
    }

    private void OnClickStartLevelButton()
    {
        UnityEngine.Debug.Log("MainMenuController / OnClickStartLevelButton");
        _sceneLoader.LoadSceneAsync(_mainMenuSceneConfig.SceneReference).Forget();
    }
}

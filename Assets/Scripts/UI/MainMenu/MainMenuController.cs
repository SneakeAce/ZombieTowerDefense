using System;
using UnityEngine;

public class MainMenuController
{
    private MainMenuView _mainMenuView;
    private MainMenuSceneConfig _mainMenuSceneConfig;
    private ISceneLoader _sceneLoader;

    private Camera _camera;
    private Canvas _mainMenuCanvas;

    public MainMenuController(MainMenuView mainMenuView, Camera camera, MainMenuSceneConfig mainMenuSceneConfig, 
        ISceneLoader sceneLoader)
    {
        UnityEngine.Debug.Log("MainMenuController");

        _mainMenuView = mainMenuView;
        _camera = camera;
        _mainMenuSceneConfig = mainMenuSceneConfig;
        _sceneLoader = sceneLoader;

        Initialize();
    }

    private void Initialize()
    {
        UnityEngine.Debug.Log("MainMenuController / Initialize");
        _mainMenuCanvas = _mainMenuView.GetComponent<Canvas>();

        if (_mainMenuCanvas == null)
            throw new NullReferenceException("MainMenuCanvas is null!");

        _mainMenuCanvas.worldCamera = _camera;
        UnityEngine.Debug.Log($"MainMenuController /  _mainMenuCanvas.worldCamera = { _mainMenuCanvas.worldCamera}");

        _mainMenuView.StartLevelButton.onClick.AddListener(OnClickStartLevelButton);
    }

    private void OnClickStartLevelButton()
    {
        UnityEngine.Debug.Log("MainMenuController / OnClickStartLevelButton");
        _sceneLoader.LoadSceneAsync(_mainMenuSceneConfig.SceneReference).Forget();
    }
}

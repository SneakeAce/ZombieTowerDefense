using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainMenuManager
{
    private AssetReference _mainMenuPrefab;

    private IMainMenuSceneAsyncObjectFactory _mainMenuSceneObjectFactory;

    private MainMenuView _mainMenuView;
    private Canvas _mainMenuCanvas;
    private DiContainer _container;

    public MainMenuManager(AssetReference mainMenuPrefab, DiContainer container,
        IMainMenuSceneAsyncObjectFactory mainMenuSceneObjectFactory)
    {
        Debug.Log("MainMenuManager constructor called.");   
        _mainMenuPrefab = mainMenuPrefab;
        _container = container;
        _mainMenuSceneObjectFactory = mainMenuSceneObjectFactory;
    }

    public async UniTask LoadMainMenuPrefabAsync()
    {
        _mainMenuCanvas = await _mainMenuSceneObjectFactory.CreateAsync<Canvas>(_mainMenuPrefab);
        _mainMenuView = _mainMenuCanvas.GetComponentInChildren<MainMenuView>();

        if (_mainMenuView == null)
            throw new NullReferenceException("MainMenuView component is missing on the main menu prefab!");

        if (_mainMenuCanvas == null)
            throw new NullReferenceException("MainMenuCanvas is null!");

        BindMainMenuView();
    }

    private void BindMainMenuView()
    {
        Debug.Log("BindMainMenuView");

        _container.Bind<MainMenuView>()
            .FromInstance(_mainMenuView)
            .AsSingle();
    }   
}

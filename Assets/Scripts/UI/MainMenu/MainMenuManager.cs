using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainMenuManager : IMainMenuManager
{
    private AssetReference _mainMenuCanvasPrefab;
    private Vector3 _spawnPositionCanvas = new Vector3(0f, 0f, 0f);

    private IAsyncObjectFactory _mainMenuSceneObjectFactory;

    private MainMenuView _mainMenuView;
    private Canvas _mainMenuCanvas;

    public MainMenuManager(AssetReference mainMenuPrefab, IAsyncObjectFactory mainMenuSceneObjectFactory)
    {
        Debug.Log("MainMenuManager constructor called.");   
        _mainMenuCanvasPrefab = mainMenuPrefab;
        _mainMenuSceneObjectFactory = mainMenuSceneObjectFactory;
    }

    public MainMenuView MainMenuView => _mainMenuView;
    public Canvas MainMenuCanvas => _mainMenuCanvas;

    public async UniTask LoadPrefabAsync()
    {
        ObjectSpawnArguments objectSpawnArguments = new ObjectSpawnArguments(_mainMenuCanvasPrefab,
            _spawnPositionCanvas, Quaternion.identity);

        _mainMenuCanvas = await _mainMenuSceneObjectFactory.CreateAsync<Canvas, ObjectSpawnArguments>(objectSpawnArguments);

        if (_mainMenuCanvas == null)
            throw new NullReferenceException("MainMenuCanvas is null!");

        _mainMenuView = _mainMenuCanvas.GetComponentInChildren<MainMenuView>();

        if (_mainMenuView == null)
            throw new NullReferenceException("MainMenuView component is missing on the main menu prefab!");
    }
}

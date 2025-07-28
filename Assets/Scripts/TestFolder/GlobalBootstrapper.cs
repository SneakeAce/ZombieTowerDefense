using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalBootstrapper : IInitializable
{
    private PlayerInputManager _playerInputManager;
    private MainMenuSceneBootstrapper _mainMenuBootstrapper;
    private ConfigsLoader _configsLoader;

    private IConfigsProvider _configsProvider;
    private IInitializer _initializer;

    private List<IInitialize> _initializeList = new List<IInitialize>();

    public GlobalBootstrapper(IConfigsProvider configsProvider, IInitializer initializer, 
        MainMenuSceneBootstrapper mainMenuBootstrapper,
        ConfigsLoader configsLoader, PlayerInputManager playerInputManager)
    {
        _configsProvider = configsProvider;
        _initializer = initializer;

        _mainMenuBootstrapper = mainMenuBootstrapper;
        _configsLoader = configsLoader;
        _playerInputManager = playerInputManager;
    }

    public void Initialize()
    {
        FillInitializeList();

        LoadManagersAsync().Forget();
    }

    private void FillInitializeList()
    {
        _initializeList.Add(_playerInputManager);
        _initializeList.Add(_mainMenuBootstrapper);
    }

    private async UniTask LoadManagersAsync()
    {
        await LoadAllConfigs();

        _initializer.Initialize(_initializeList);

        Debug.Log("GlobalBootstrapper LoadManagersAsync end");
    }

    private async UniTask LoadAllConfigs()
    {
        Dictionary<Type, object> librariesConfigs = await _configsLoader.LoadAllLibraryConfigsAsync();
        Dictionary<Type, object> singleConfigs = await _configsLoader.LoadAllSingleConfigsAsync();
        Dictionary<SceneId, ISceneConfig> sceneConfigs = await _configsLoader.LoadAllSceneConfigsAsync();

        _configsProvider.AddAllLLibraryConfigs(librariesConfigs);
        _configsProvider.AddAllSingleConfigs(singleConfigs);
        _configsProvider.AddAllSceneConfigs(sceneConfigs);
    }
}

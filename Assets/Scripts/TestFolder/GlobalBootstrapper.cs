using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalBootstrapper : IInitializable
{
    private PlayerInputManager _playerInputManager;

    private IConfigsProvider _configsProvider;
    private IInitializer _initializer;
    private ConfigsLoader _configsLoader;

    public GlobalBootstrapper(IConfigsProvider configsProvider, ConfigsLoader configsLoader,
        PlayerInputManager playerInputManager, IInitializer initializer)
    {
        _configsProvider = configsProvider;
        _configsLoader = configsLoader;
        _playerInputManager = playerInputManager;
        _initializer = initializer;
    }

    public void Initialize()
    {
        LoadManagersAsync().Forget();

        _initializer.Initialize(_playerInputManager);
    }

    private async UniTask LoadManagersAsync()
    {
        await LoadAllConfigs();

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

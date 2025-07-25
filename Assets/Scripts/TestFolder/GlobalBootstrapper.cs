using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalBootstrapper : IInitializable
{
    private IConfigsProvider _configsProvider;

    private ConfigsLoader _configsLoader;

    public GlobalBootstrapper(IConfigsProvider configsProvider, ConfigsLoader configsLoader)
    {
        _configsProvider = configsProvider;
        _configsLoader = configsLoader;
    }

    public void Initialize()
    {
        LoadManagersAsync().Forget();
    }

    private async UniTask LoadManagersAsync()
    {
        await LoadLibrariesConfigs();

        Debug.Log("GlobalBootstrapper LoadManagersAsync end");
    }

    private async UniTask LoadLibrariesConfigs()
    {
        Dictionary<Type, object> librariesConfigs = await _configsLoader.LoadAllLibraryConfigsAsync();

        _configsProvider.AddAllLLibraryConfigs(librariesConfigs);
    }
}

using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ConfigsLoader
{
    private readonly AssetLabelReference _configsLibraryLabel;
    private readonly AssetLabelReference _singleConfigLabel;
    private readonly AssetLabelReference _sceneConfigLabel;

    public ConfigsLoader(AssetLabelReference configsLibraryLabel, AssetLabelReference configLabel,
        AssetLabelReference sceneConfigLabel)
    {
        _configsLibraryLabel = configsLibraryLabel;
        _singleConfigLabel = configLabel;
        _sceneConfigLabel = sceneConfigLabel;
    }

    public async UniTask<Dictionary<Type, object>> LoadAllLibraryConfigsAsync()
    {
        var result = new Dictionary<Type, object>();

        var handle = Addressables.LoadAssetsAsync<ScriptableObject>(_configsLibraryLabel, null);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigsLoader] Error loading config libraries with a label: {_configsLibraryLabel.ToString()}");
            return result;
        }

        for (int i = 0; i < handle.Result.Count;  i++)
        {
            var configLibrary = handle.Result[i];
            var interfaces = configLibrary.GetType().GetInterfaces();

            for (int j = 0; j < interfaces.Length; j++) 
            { 
                var currentInterface = interfaces[j];

                if (currentInterface.IsGenericType 
                    && currentInterface.GetGenericTypeDefinition() == typeof(ILibraryConfigs<>))
                {
                    var configType = currentInterface.GetGenericArguments()[0];

                    if (result.ContainsKey(configType))
                        continue;

                    result[configType] = configLibrary;
                    break;
                }
            }
        }

        Addressables.Release(handle);
        return result;
    }

    public async UniTask<Dictionary<Type, object>> LoadAllSingleConfigsAsync()
    {
        var result = new Dictionary<Type, object>();

        var handle = Addressables.LoadAssetsAsync<ScriptableObject>(_singleConfigLabel, null);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigsLoader] Error loading config with a label: {_singleConfigLabel.ToString()}");
            return result;
        }

        for (int i = 0; i < handle.Result.Count; i++)
        {
            var currentConfig = handle.Result[i];
            var typeConfig = currentConfig.GetType();

            if (currentConfig is IConfig typedConfig)
            {
                if (result.ContainsKey(typeConfig))
                    continue;

                result[typeConfig] = typedConfig;
            }
            else
            {
                Debug.LogWarning($"[ConfigsLoader] Config {currentConfig.name} does not implement IConfig.");
            }
        }

        return result;
    }

    public async UniTask<Dictionary<SceneId, ISceneConfig>> LoadAllSceneConfigsAsync()
    {
        var result = new Dictionary<SceneId, ISceneConfig>();

        var handle = Addressables.LoadAssetsAsync<ScriptableObject>(_sceneConfigLabel, null);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigsLoader] Error loading config with a label: {_sceneConfigLabel.ToString()}");
            return result;
        }

        for (int i = 0; i < handle.Result.Count; i++)
        {
            var currentConfig = handle.Result[i];

            if (currentConfig is ISceneConfig typedConfig)
            {
                if (result.ContainsKey(typedConfig.SceneId))
                    continue;

                result[typedConfig.SceneId] = typedConfig;
            }
            else
            {
                Debug.LogWarning($"[ConfigsLoader] Config {currentConfig.name} does not implement IConfig.");
            }
        }

        return result;
    }
}

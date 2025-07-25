using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ConfigsLoader
{
    private readonly AssetLabelReference _configsLibraryLabel;

    public ConfigsLoader(AssetLabelReference configsLibraryLabel)
    {
        _configsLibraryLabel = configsLibraryLabel;
    }

    public async UniTask<Dictionary<Type, object>> LoadAllLibraryConfigsAsync()
    {
        var result = new Dictionary<Type, object>();

        var handle = Addressables.LoadAssetsAsync<ScriptableObject>(_configsLibraryLabel, null);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigsLoader] Ошибка загрузки библиотек конфигов с лейблом: {_configsLibraryLabel.ToString()}");
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
                        break;

                    result[configType] = configLibrary;
                    break;
                }
            }
        }

        Addressables.Release(handle);
        return result;
    }
}

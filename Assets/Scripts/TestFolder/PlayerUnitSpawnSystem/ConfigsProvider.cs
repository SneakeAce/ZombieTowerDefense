using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigsProvider : IConfigsProvider
{
    private Dictionary<Type, object> _librariesConfigs;
    private Dictionary<Type, object> _singleConfigs;
    private Dictionary<SceneId, ISceneConfig> _sceneConfigs;

    public void AddAllLLibraryConfigs(Dictionary<Type, object> librariesConfigs)
    {
        if (librariesConfigs == null)
            throw new NullReferenceException($"[ConfigsProvider] AddAllLLibraryConfigs - librariesConfigs is null! Check this code!");

        _librariesConfigs = librariesConfigs;

        Debug.Log("AddAllLibrariesConfigs its done!");
    }

    public void AddAllSingleConfigs(Dictionary<Type, object> singleConfigs)
    {
        if (singleConfigs == null)
            throw new NullReferenceException($"[ConfigsProvider] AddAllConfigs - configs is null! Check this code!");

        _singleConfigs = singleConfigs;

        Debug.Log("AddAllSingleConfigs its done!");
    }

    public void AddAllSceneConfigs(Dictionary<SceneId, ISceneConfig> sceneConfigs)
    {
        if (sceneConfigs == null)
            throw new NullReferenceException($"[ConfigsProvider] AddAllConfigs - configs is null! Check this code!");

        _sceneConfigs = sceneConfigs;

        Debug.Log("AddAllSingleConfigs its done!");
    }

    public ILibraryConfigs<T> GetConfigsLibrary<T>() where T : ScriptableObject
    {
        var type = typeof(T);

        if (_librariesConfigs.TryGetValue(type, out var library))
        {
            if (library is ILibraryConfigs<T> typedLibrary)
                return typedLibrary;

            throw new InvalidCastException($"Library for type {type} does not implement ILibraryConfigs<{type}>.");
        }

        throw new KeyNotFoundException($"No config library found for type {type}.");
    }

    public T GetSingleConfig<T>() where T : class, IConfig
    {
        var type = typeof(T);

        if (_singleConfigs.TryGetValue(type, out var config))
            return config as T;

        throw new Exception($"[ConfigsProvider] Config of type {typeof(T).Name} not found.");
    }

    public ISceneConfig GetSceneConfig()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (Enum.TryParse<SceneId>(currentSceneName, out var sceneId))
            return GetSceneConfig(sceneId);
        
        throw new Exception($"[ConfigsProvider] No SceneId matches scene name '{currentSceneName}'.");
    }

    private ISceneConfig GetSceneConfig(SceneId sceneId)
    {
        if (_sceneConfigs.TryGetValue(sceneId, out var config))
            return config;

        throw new Exception($"[ConfigsProvider] Config of type {sceneId} not found.");
    }
}

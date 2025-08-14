using System;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigsProvider
{
    void AddAllLLibraryConfigs(Dictionary<Type, object> librariesConfigs);
    void AddAllSingleConfigs(Dictionary<Type, object> singleConfigs);
    void AddAllSceneConfigs(Dictionary<SceneId, ISceneConfig> sceneConfigs);

    ILibraryConfigs<T> GetConfigsLibrary<T>() where T : ScriptableObject;
    T GetSingleConfig<T>() where T : class, IConfig;
    ISceneConfig GetSceneConfig();
}

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigsProvider
{
    void AddAllLLibraryConfigs(Dictionary<Type, object> librariesConfigs);

    ILibraryConfigs<T> GetLibrary<T>() where T : ScriptableObject;
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsProvider : IConfigsProvider
{
    private Dictionary<Type, object> _librariesConfigs;

    public void AddAllLLibraryConfigs(Dictionary<Type, object> librariesConfigs)
    {
        if (librariesConfigs == null)
            throw new NullReferenceException($"[ConfigsProvider] InitializeConfigs - librariesConfigs is null! Check this code!");

        _librariesConfigs = librariesConfigs;

        Debug.Log("AddAllLibrariesConfigs its done!");
    }

    public ILibraryConfigs<T> GetLibrary<T>() where T : ScriptableObject
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
}

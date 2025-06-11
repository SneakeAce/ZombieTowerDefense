using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class SceneObjectFactory : IAsyncObjectFactory
{
    public async UniTask<T> CreateAsync<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not ObjectSpawnArguments objectSpawnArgs)
            throw new ArgumentException("Invalid arguments provided for SceneObjectFactory.");

        GameObject instance = await objectSpawnArgs.AssetReference.InstantiateAsync(objectSpawnArgs.SpawnPosition, objectSpawnArgs.SpawnRotation, null);

        if (instance == null)
            throw new NullReferenceException("Instance Object in MainMenuFactory is Null!");

        return instance.GetComponent<T>();
    }
}

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class SceneObjectFactory : ISceneAsyncObjectFactory
{
    public async UniTask<T> CreateAsync<T>(AssetReference reference, Vector3 spawnPosition, Quaternion rotation) where T : Object
    {
        GameObject instance = await reference.InstantiateAsync(spawnPosition, rotation, null);

        if (instance == null)
            throw new NullReferenceException("Instance Object in MainMenuFactory is Null!");

        return instance.GetComponent<T>();
    }
}

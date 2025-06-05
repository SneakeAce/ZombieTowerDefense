using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class MainMenuSceneObjectFactory : IMainMenuSceneAsyncObjectFactory
{
    public async UniTask<T> CreateAsync<T>(AssetReference reference) where T : Object
    {
        GameObject instance = await reference.InstantiateAsync();

        if (instance == null)
            throw new NullReferenceException("Instance Object in MainMenuFactory is Null!");

        return instance.GetComponent<T>();
    }
}

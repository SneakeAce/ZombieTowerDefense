using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class AssetProvider : IAssetProvider
{
    public async UniTask<T> LoadAssetAsync<T>(string key) where T : Object
    {
        var handle = Addressables.LoadAssetAsync<T>(key);

        await handle.ToUniTask();

        return handle.Result;
    }
}

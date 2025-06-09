using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAsyncFactory 
{
    UniTask<T> CreateAsync<T>(AssetReference reference, Vector3 spawnPosition, Quaternion rotation) where T : Object;
}

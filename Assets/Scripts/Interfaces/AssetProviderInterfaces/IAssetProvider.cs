using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAssetProvider
{
    UniTask<T> LoadAssetAsync<T>(string key) where T : Object;
}

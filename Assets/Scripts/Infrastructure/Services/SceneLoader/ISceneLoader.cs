using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public interface ISceneLoader
{
    UniTaskVoid LoadSceneAsync(AssetReference sceneReference);
}

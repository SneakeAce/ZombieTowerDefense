using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class FirstLevelSceneLoader : ISceneLoader
{
    private IAssetProvider _assetProvider;

    public FirstLevelSceneLoader(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTaskVoid LoadSceneAsync(AssetReference sceneReference)
    {
        await Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Single).ToUniTask();
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class FirstLevelSceneLoader : ISceneLoader
{
    public FirstLevelSceneLoader()
    {
    }

    public async UniTaskVoid LoadSceneAsync(AssetReference sceneReference)
    {
        await Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Single).ToUniTask();
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FirstLevelSceneBootstrapper : IInitializable
{
    private CameraManager _cameraManager;

    public FirstLevelSceneBootstrapper(CameraManager cameraManager)
    {
        _cameraManager = cameraManager;
    }

    public void Initialize()
    {
        Debug.Log("FirstLevelSceneBootstrapper Initialize");

        LoadManagersAsync().Forget();
    }

    private async UniTask LoadManagersAsync()
    {
        await _cameraManager.LoadAndCreateCameraAsync();

        Debug.Log("FirstLevelSceneBootstrapper LoadManagersAsync end");

    }
}

using System;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

public class CameraManager
{
    private AssetReference _mainCameraPrefab;
    private AssetReference _virtualCameraPrefab;

    private IMainMenuSceneAsyncObjectFactory _mainMenuSceneObjectFactory;

    private Camera _mainCamera;
    private CinemachineCamera _cinemachineVirtualCamera;
    private DiContainer _container;

    public CameraManager(AssetReference cameraPrefab, AssetReference virtualCameraPrefab, DiContainer container,
        IMainMenuSceneAsyncObjectFactory mainMenuSceneObjectFactory)
    {
        Debug.Log("CameraManager constructor called.");

        _mainCameraPrefab = cameraPrefab;
        _virtualCameraPrefab = virtualCameraPrefab;
        _container = container;
        _mainMenuSceneObjectFactory = mainMenuSceneObjectFactory;
    }

    public async UniTask LoadAndCreateCameraAsync()
    {
        _mainCamera = await _mainMenuSceneObjectFactory.CreateAsync<Camera>(_mainCameraPrefab);

        _mainCamera.tag = "MainCamera";

        if (_mainCamera.GetComponent<CinemachineBrain>() == null)
            throw new NullReferenceException("CinemachineBrain on Camera is missing!");

        BindCamera(_mainCamera);

        _cinemachineVirtualCamera = await _mainMenuSceneObjectFactory.CreateAsync<CinemachineCamera>(_virtualCameraPrefab);

        if (_cinemachineVirtualCamera == null)
            throw new NullReferenceException("CinemachineCamera component is missing on the virtual camera prefab!");

        BindCamera(_cinemachineVirtualCamera);

        //Повесить персонажа на камеру.
    } 

    private void BindCamera<T>(T prefab) where T : Object
    {
        _container.Bind<T>()
            .FromInstance(prefab)
            .AsSingle();
    }

}

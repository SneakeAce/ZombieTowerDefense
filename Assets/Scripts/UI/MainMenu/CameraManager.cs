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

    private SpawnCameraData _spawnCameraData;

    private ISceneAsyncObjectFactory _sceneObjectFactory;

    private Camera _mainCamera;
    private CinemachineCamera _cinemachineVirtualCamera;
    private DiContainer _container;

    public CameraManager(SpawnCameraData spawnCameraData, DiContainer container, ISceneAsyncObjectFactory sceneObjectFactory)
    {
        Debug.Log("CameraManager constructor called.");
        _spawnCameraData = spawnCameraData;

        _mainCameraPrefab = _spawnCameraData.CameraPrefab;
        _virtualCameraPrefab = _spawnCameraData.VirtualCameraPrefab;

        _container = container;
        _sceneObjectFactory = sceneObjectFactory;
    }

    public async UniTask LoadAndCreateCameraAsync()
    {
        _mainCamera = await _sceneObjectFactory.CreateAsync<Camera>(_mainCameraPrefab, 
            _spawnCameraData.MainCameraSpawnPosition, _spawnCameraData.MainCameraRotation);

        _mainCamera.tag = "MainCamera";

        if (_mainCamera.GetComponent<CinemachineBrain>() == null)
            throw new NullReferenceException("CinemachineBrain on Camera is missing!");

        BindCamera(_mainCamera);

        _cinemachineVirtualCamera = await _sceneObjectFactory.CreateAsync<CinemachineCamera>(_virtualCameraPrefab,
            _spawnCameraData.VirtualCameraSpawnPosition, _spawnCameraData.VirtualCameraRotation);

        if (_cinemachineVirtualCamera == null)
            throw new NullReferenceException("CinemachineCamera component is missing on the virtual camera prefab!");

        BindCamera(_cinemachineVirtualCamera);

        //сделать контроллер для камеры, где будет вешаться персонаж
    } 

    private void BindCamera<T>(T prefab) where T : Object
    {
        _container.Bind<T>()
            .FromInstance(prefab)
            .AsSingle();
    }

}

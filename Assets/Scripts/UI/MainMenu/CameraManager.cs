using System;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class CameraManager
{
    private SpawnCameraData _spawnCameraData;

    private IAsyncObjectFactory _sceneObjectFactory;

    private Camera _mainCamera;
    private CinemachineCamera _cinemachineVirtualCamera;
    private DiContainer _container;

    public CameraManager(SpawnCameraData spawnCameraData, DiContainer container, IAsyncObjectFactory sceneObjectFactory)
    {
        Debug.Log("CameraManager constructor called.");
        _spawnCameraData = spawnCameraData;

        _container = container;
        _sceneObjectFactory = sceneObjectFactory;
    }

    public async UniTask LoadAndCreateCameraAsync()
    {
        await CreateMainCamera();

        await CreateVirtualCamera();

        //сделать контроллер для камеры, где будет вешаться персонаж
    } 

    private async UniTask CreateMainCamera()
    {
        ObjectSpawnArguments cameraSpawnArguments = new ObjectSpawnArguments(_spawnCameraData.CameraPrefab,
     _spawnCameraData.MainCameraSpawnPosition, _spawnCameraData.MainCameraRotation);

        _mainCamera = await _sceneObjectFactory.CreateAsync<Camera, ObjectSpawnArguments>(cameraSpawnArguments);

        _mainCamera.tag = "MainCamera";

        if (_mainCamera.GetComponent<CinemachineBrain>() == null)
            throw new NullReferenceException("CinemachineBrain on Camera is missing!");

        BindCamera(_mainCamera);
    }

    private async UniTask CreateVirtualCamera()
    {
        ObjectSpawnArguments virtualCameraSpawnArguments = new ObjectSpawnArguments(_spawnCameraData.VirtualCameraPrefab,
            _spawnCameraData.VirtualCameraSpawnPosition, _spawnCameraData.VirtualCameraRotation);

        _cinemachineVirtualCamera = await _sceneObjectFactory.CreateAsync<CinemachineCamera, ObjectSpawnArguments>(virtualCameraSpawnArguments);

        if (_cinemachineVirtualCamera == null)
            throw new NullReferenceException("CinemachineCamera component is missing on the virtual camera prefab!");

        BindCamera(_cinemachineVirtualCamera);
    }

    private void BindCamera<T>(T prefab) where T : Object
    {
        _container.Bind<T>()
            .FromInstance(prefab)
            .AsSingle();
    }

}

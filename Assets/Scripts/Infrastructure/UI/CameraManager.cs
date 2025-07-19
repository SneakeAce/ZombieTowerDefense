using System;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : ICameraManager
{
    private SpawnCameraConfig _spawnCameraData;

    private IAsyncObjectFactory _sceneObjectFactory;

    private Camera _mainCamera;
    private CinemachineCamera _cinemachineVirtualCamera;

    public CameraManager(SpawnCameraConfig spawnCameraData, IAsyncObjectFactory sceneObjectFactory)
    {
        _spawnCameraData = spawnCameraData;

        _sceneObjectFactory = sceneObjectFactory;
    }

    public Camera MainCamera => _mainCamera;
    public CinemachineCamera VirtualCamera => _cinemachineVirtualCamera;

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
    }

    private async UniTask CreateVirtualCamera()
    {
        ObjectSpawnArguments virtualCameraSpawnArguments = new ObjectSpawnArguments(_spawnCameraData.VirtualCameraPrefab,
            _spawnCameraData.VirtualCameraSpawnPosition, _spawnCameraData.VirtualCameraRotation);

        _cinemachineVirtualCamera = await _sceneObjectFactory.CreateAsync<CinemachineCamera, ObjectSpawnArguments>(virtualCameraSpawnArguments);

        if (_cinemachineVirtualCamera == null)
            throw new NullReferenceException("CinemachineCamera component is missing on the virtual camera prefab!");
    }
}

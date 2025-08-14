using System;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : ICameraManager
{
    private SpawnCameraStats _spawnCameraData;

    private IAsyncObjectFactory _sceneObjectFactory;
    private IConfigsProvider _configsProvider;

    private Camera _mainCamera;
    private CinemachineCamera _cinemachineVirtualCamera;

    public CameraManager(IAsyncObjectFactory sceneObjectFactory, IConfigsProvider configsProvider)
    {
        _sceneObjectFactory = sceneObjectFactory;
        _configsProvider = configsProvider;
    }

    public Camera MainCamera => _mainCamera;
    public CinemachineCamera VirtualCamera => _cinemachineVirtualCamera;

    public async UniTask LoadAndCreateCameraAsync()
    {
        GetConfig();

        await CreateMainCamera();

        await CreateVirtualCamera();
    }

    private void GetConfig()
    {
        var config = _configsProvider.GetSceneConfig();

        _spawnCameraData = config.CameraData;
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

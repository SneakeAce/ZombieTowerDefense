using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class SpawnCameraConfig
{
    [field: SerializeField] public AssetReference CameraPrefab { get; private set; }
    [field: SerializeField] public Vector3 MainCameraSpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion MainCameraRotation { get; private set; }

    [field: SerializeField] public AssetReference VirtualCameraPrefab { get; private set; }
    [field: SerializeField] public Vector3 VirtualCameraSpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion VirtualCameraRotation { get; private set; }
}

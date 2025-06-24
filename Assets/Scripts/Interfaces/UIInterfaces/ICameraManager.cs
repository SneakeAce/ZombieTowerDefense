using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public interface ICameraManager
{
    public Camera MainCamera { get; }
    public CinemachineCamera VirtualCamera { get; }

    UniTask LoadAndCreateCameraAsync();
}

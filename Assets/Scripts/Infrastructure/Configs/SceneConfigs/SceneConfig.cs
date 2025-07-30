using UnityEngine;

public abstract class SceneConfig : ScriptableObject, ISceneConfig
{
    [field: SerializeField] public SpawnCameraStats SpawnCameraData { get; private set; }
    [field: SerializeField] public SceneId Id { get; private set; }

    public SceneId SceneId => Id;

    public SpawnCameraStats CameraData => SpawnCameraData;
}

using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Configs/SceneConfigs/MainMenuSceneConfig", fileName = "MainMenuSceneConfig")]
public class MainMenuSceneConfig : SceneConfig
{
    [field: SerializeField] public AssetReference SceneReference { get; private set; }
    [field: SerializeField] public AssetReference MainMenuCanvasPrefab { get; private set; }
    [field: SerializeField] public SpawnCameraData SpawnCameraData { get; private set; }
}

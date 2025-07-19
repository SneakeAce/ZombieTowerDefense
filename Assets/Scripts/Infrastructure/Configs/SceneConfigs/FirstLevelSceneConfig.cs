using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Configs/SceneConfigs/FirstLevelSceneConfig", fileName = "FirstLevelSceneConfig")]
public class FirstLevelSceneConfig : SceneConfig
{
    [field: SerializeField] public SpawnCameraConfig SpawnCameraData { get; private set; }
}

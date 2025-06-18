using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Configs/SceneConfigs/FirstLevelSceneConfig", fileName = "FirstLevelSceneConfig")]
public class FirstLevelSceneConfig : SceneConfig
{
    [field: SerializeField] public SpawnCameraData SpawnCameraData { get; private set; }
}

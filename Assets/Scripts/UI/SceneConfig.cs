using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class SceneConfig : ScriptableObject
{
    [field: SerializeField] public AssetReference SceneReference { get; private set; }
}

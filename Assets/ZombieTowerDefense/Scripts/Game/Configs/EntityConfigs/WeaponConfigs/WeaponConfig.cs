using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Configs/Weapon", fileName = "TestWeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public Weapon Prefab { get; private set; }
    [field: SerializeField] public WeaponStats WeaponStats { get; private set; }
}

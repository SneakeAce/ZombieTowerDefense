using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/WeaponConfigsLibrary", fileName = "TestWeaponConfigsLibrary")]
public class WeaponConfigsLibrary : ScriptableObject, ILibraryConfigs<WeaponConfig>
{
    [field: SerializeField] public List<WeaponConfig> WeaponConfigs { get; private set; }

    public List<WeaponConfig> Configs => WeaponConfigs;
}

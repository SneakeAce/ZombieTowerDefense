using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/UnitHiringConfigsLibrary")]
public class UnitHiringConfigsLibrary : ScriptableObject, ILibraryConfigs<UnitHiringConfig>
{
    [SerializeField] public List<UnitHiringConfig> UnitHiringConfigs { get; private set; }

    public List<UnitHiringConfig> Configs => UnitHiringConfigs;
}

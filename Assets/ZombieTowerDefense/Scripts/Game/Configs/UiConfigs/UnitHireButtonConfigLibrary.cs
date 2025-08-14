using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HireUnitButtonConfig/UnitHireButtonConfigsLibrary", fileName = "UnitHireButtonConfigsLibrary")]

public class UnitHireButtonConfigsLibrary : ScriptableObject, ILibraryConfigs<UnitHireButtonConfig>
{
    [field: SerializeField] public List<UnitHireButtonConfig> Configs { get; private set; }
}

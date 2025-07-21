using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HireUnitButtonConfig/HireUnitButtonConfigsLibrary", fileName = "HireUnitButtonConfigsLibrary")]

public class UnitHireButtonConfigsLibrary : ScriptableObject
{
    [field: SerializeField] public List<UnitHireButtonConfig> Configs { get; private set; }
}

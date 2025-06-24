using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HireUnitButtonConfig/HireUnitButtonConfigsLibrary", fileName = "HireUnitButtonConfigsLibrary")]

public class HireUnitButtonConfigsLibrary : ScriptableObject
{
    [field: SerializeField] public List<HireUnitButtonConfig> Configs { get; private set; }
}

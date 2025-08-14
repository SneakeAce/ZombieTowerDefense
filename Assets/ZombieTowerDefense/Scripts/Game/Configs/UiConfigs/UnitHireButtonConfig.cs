using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HireUnitButtonConfig/HireUnitButtonConfig", fileName = "HireUnitButtonConfig")]
public class UnitHireButtonConfig : ScriptableObject
{
    [field: SerializeField] public PlayerUnitType UnitType { get; private set; }
}

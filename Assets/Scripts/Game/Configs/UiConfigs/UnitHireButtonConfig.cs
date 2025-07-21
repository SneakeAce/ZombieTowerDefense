using UnityEngine;

[CreateAssetMenu(menuName = "Configs/HireUnitButtonConfig/HireUnitButtonConfig", fileName = "HireUnitButtonConfig")]
public class UnitHireButtonConfig : ScriptableObject
{
    [field: SerializeField] public UnitType UnitType { get; private set; }
    [field: SerializeField] public float HiringTime { get; private set; }
    [field: SerializeField] public int HiringCost { get; private set; }
}

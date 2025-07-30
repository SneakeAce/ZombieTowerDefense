using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/UnitConfigs/PlayerUnitConfig")]
public class PlayerUnitConfig : ScriptableObject
{
    [field: SerializeField] public UnitMainStats UnitMainStats { get; private set; }
}

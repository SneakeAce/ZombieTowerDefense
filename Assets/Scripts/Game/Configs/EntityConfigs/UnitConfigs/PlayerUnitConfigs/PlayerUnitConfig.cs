using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/UnitConfigs/PlayerUnitConfig")]
public class PlayerUnitConfig : UnitConfig
{
    [field: SerializeField] public UnitHiringStats UnitHiringStats { get; private set; }
}

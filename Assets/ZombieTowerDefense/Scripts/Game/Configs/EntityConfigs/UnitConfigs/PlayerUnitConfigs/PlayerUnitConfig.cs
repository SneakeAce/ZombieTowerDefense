using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/UnitConfigs/PlayerUnitConfig")]
public class PlayerUnitConfig : UnitConfig, IUnitConfig<PlayerUnitType>
{
    [field: SerializeField] public UnitWeaponSpawnStats UnitWeapon { get; private set; }
    [field: SerializeField] public UnitHiringStats UnitHiringStats { get; private set; }
    [field: SerializeField] public PlayerUnitType Type { get; private set; }

    public PlayerUnitType UnitType => Type;
}

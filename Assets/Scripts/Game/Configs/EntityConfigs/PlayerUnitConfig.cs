using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Configs/UnitConfig/TESTCONFIG", fileName = "TESTMarineConfig")]
public class PlayerUnitConfig : ScriptableObject
{
    [field: SerializeField] public AssetReference Prefab { get; private set; }
    [field: SerializeField] public UnitType UnitType { get; private set; }

    [field: SerializeField] public MoveStats MoveStats { get; private set; }
    [field: SerializeField] public HealthStats HealthStats { get; private set; }
    [field: SerializeField] public AttackStats AttackStats { get; private set; }

    public UnitType CurrentUnitType => UnitType;
}
